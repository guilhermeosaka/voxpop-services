using Dapper;
using Voxpop.Core.Application.Polls.Dtos;
using Voxpop.Core.Application.Polls.Models;
using Voxpop.Core.Application.Polls.Queries;
using Voxpop.Core.Contracts.Enums;
using Voxpop.Core.Infrastructure.Persistence.Common.Dapper;
using Voxpop.Core.Infrastructure.Persistence.Polls.Queries.Results;

namespace Voxpop.Core.Infrastructure.Persistence.Polls.Queries;

public class PollQueries(ISqlConnectionFactory connectionFactory) : IPollQueries
{
    public async Task<IReadOnlyList<PollSummary>> GetPollsAsync(
        int page,
        int pageSize,
        PollSortBy sortBy,
        Guid? userId,
        bool? createdByMe,
        PollStatus? status,
        VoteMode? voteMode,
        bool? votedByMe,
        CancellationToken ct = default)
    {
        var db = connectionFactory.CreateConnection();

        var parameters = new DynamicParameters();
        parameters.Add("@Page", page);
        parameters.Add("@PageSize", pageSize);
        parameters.Add("@UserId", userId);

        #region WHERE clause

        var whereConditions = new List<string> { "not p.is_archived" };
        var votedByMeJoinClause = string.Empty;

        if (createdByMe.HasValue && userId.HasValue)
        {
            whereConditions.Add(createdByMe.Value ? "p.created_by = @UserId" : "p.created_by != @UserId");
        }

        if (votedByMe.HasValue && userId.HasValue)
        {
            votedByMeJoinClause = votedByMe.Value
                ? "JOIN user_voted_polls uvp ON uvp.poll_id = p.id"
                : "LEFT JOIN user_voted_polls uvp ON uvp.poll_id = p.id";
            
            if (!votedByMe.Value)
                whereConditions.Add("uvp.id IS NULL");
        }

        if (status.HasValue)
        {
            switch (status.Value)
            {
                case PollStatus.Open:
                    whereConditions.Add("(not p.is_closed AND (p.expires_at IS NULL OR p.expires_at > NOW()))");
                    break;
                case PollStatus.Closed:
                    whereConditions.Add("(p.is_closed OR (p.expires_at IS NOT NULL AND p.expires_at <= NOW()))");
                    break;
            }
        }

        if (voteMode.HasValue)
        {
            whereConditions.Add("p.vote_mode = @VoteMode");
            parameters.Add("@VoteMode", (int)voteMode.Value);
        }

        #endregion

        #region ORDER BY clause

        const string defaultSort = "total_votes DESC, p.created_at DESC";
        var orderByClause = sortBy switch
        {
            PollSortBy.CreatedAtAsc => "p.created_at ASC",
            PollSortBy.CreatedAtDesc => "p.created_at DESC",
            PollSortBy.TotalVotesDesc => defaultSort,
            PollSortBy.TotalVotesAsc => "total_votes ASC, p.created_at DESC",
            PollSortBy.ExpiresAtAsc =>
                "CASE WHEN p.expires_at IS NULL THEN 1 ELSE 0 END, p.expires_at ASC, p.created_at DESC",
            PollSortBy.ExpiresAtDesc =>
                "CASE WHEN p.expires_at IS NULL THEN 1 ELSE 0 END, p.expires_at DESC, p.created_at DESC",
            _ => defaultSort
        };

        var whereClause = string.Join(" AND ", whereConditions);

        #endregion

        var sql = $"""
                   WITH poll_vote_counts AS (
                       SELECT
                           po.poll_id,
                           COUNT(DISTINCT v.user_id) as total_votes
                       FROM poll_options po
                       LEFT JOIN votes v ON v.option_id = po.id
                       GROUP BY po.poll_id
                   ),
                   user_voted_polls AS (
                       SELECT DISTINCT po.poll_id
                       FROM poll_options po
                       JOIN votes v ON v.option_id = po.id
                       WHERE v.user_id = @UserId
                   ),
                   paged_polls AS (
                       SELECT
                           p.id,
                           p.question,
                           p.vote_mode,
                           p.expires_at,
                           p.is_closed,
                           p.created_at,
                           p.created_by = @UserId as has_created,
                           COALESCE(pvc.total_votes, 0) as total_votes,
                           ROW_NUMBER() OVER (ORDER BY {orderByClause}) AS row_number
                       FROM polls p
                       LEFT JOIN poll_vote_counts pvc ON pvc.poll_id = p.id
                       {votedByMeJoinClause}
                       WHERE {whereClause}
                       ORDER BY {orderByClause}
                       LIMIT @PageSize
                       OFFSET (@Page - 1) * @PageSize
                   )
                   SELECT
                       pp.id                AS "Id",
                       pp.question          AS "Question",
                       pp.vote_mode         AS "VoteMode",
                       pp.expires_at        AS "ExpiresAt",
                       pp.is_closed         AS "IsClosed",
                       pp.created_at        AS "CreatedAt",
                       pp.has_created       AS "HasCreated",
                       pp.total_votes       AS "TotalVotes",
                       po.id                AS "OptionId",
                       po.value             AS "OptionValue",
                       COUNT(v.id)          AS "OptionVotes",
                       uv.id IS NOT NULL    AS "OptionHasVoted"
                   FROM paged_polls pp
                   JOIN poll_options po ON po.poll_id = pp.id
                   LEFT JOIN votes v ON v.option_id = po.id
                   LEFT JOIN votes uv ON uv.option_id = po.id AND uv.user_id = @UserId
                   GROUP BY pp.id,
                            pp.question,
                            pp.vote_mode,
                            pp.expires_at,
                            pp.is_closed,
                            pp.created_at,
                            pp.has_created,
                            pp.total_votes,
                            po.id,
                            po.value,
                            po.order,
                            uv.id,
                            pp.row_number
                   ORDER BY pp.row_number, po.order;
                   """;
        
        var result = await db.QueryAsync<GetPollsResult>(sql, parameters);

        var lookup = new Dictionary<Guid, PollSummary>();

        foreach (var poll in result)
        {
            if (!lookup.TryGetValue(poll.Id, out var pollDto))
            {
                pollDto = new PollSummary(
                    poll.Id,
                    poll.Question,
                    poll.VoteMode,
                    poll.ExpiresAt,
                    poll.IsClosed,
                    poll.CreatedAt,
                    poll.HasCreated,
                    poll.TotalVotes,
                    []);
                lookup.Add(poll.Id, pollDto);
            }

            lookup[poll.Id].Options
                .Add(new PollOptionSummary(poll.OptionId, poll.OptionValue, poll.OptionVotes, poll.OptionHasVoted));
        }

        return lookup.Values.ToList();
    }

    public async Task<VotingInfoDto?> FindVotingInfoAsync(Guid pollId, CancellationToken ct = default)
    {
        var db = connectionFactory.CreateConnection();

        const string sql = """
                           SELECT p.vote_mode as "VoteMode", 
                                  p.expires_at as "ExpiresAt", 
                                  p.is_closed as "IsClosed"
                           FROM polls p
                           WHERE p.id = @PollId and not p.is_archived
                           LIMIT 1
                           """;

        var result = await db.QuerySingleOrDefaultAsync<GetVotingInfoResult>(
            sql,
            new { PollId = pollId }
        );

        return result == null ? null : new VotingInfoDto(result.VoteMode, result.ExpiresAt, result.IsClosed);
    }
}