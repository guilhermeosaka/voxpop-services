using Dapper;
using Voxpop.Core.Application.Polls.Dtos;
using Voxpop.Core.Application.Polls.Models;
using Voxpop.Core.Application.Polls.Queries;
using Voxpop.Core.Infrastructure.Persistence.Common.Dapper;
using Voxpop.Core.Infrastructure.Persistence.Polls.Queries.Results;

namespace Voxpop.Core.Infrastructure.Persistence.Polls.Queries;

public class PollQueries(ISqlConnectionFactory connectionFactory) : IPollQueries
{
    public async Task<IReadOnlyList<PollSummary>> GetPollsAsync(
        int page,
        int pageSize,
        Guid? userId,
        CancellationToken ct = default)
    {
        var db = connectionFactory.CreateConnection();

        const string sql = """
                           WITH paged_polls AS (
                               SELECT   p.id, 
                                        p.question, 
                                        p.vote_mode, 
                                        p.expires_at, 
                                        p.is_closed, 
                                        p.created_at, 
                                        p.created_by = @UserId as has_created
                               FROM polls p
                               WHERE not p.is_archived
                               ORDER BY p.created_at DESC
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
                               has_created          AS "HasCreated",
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
                                    po.id,
                                    po.value,
                                    po.order,
                                    uv.id
                           ORDER BY pp.created_at DESC, po.order;
                           """;

        var result = await db.QueryAsync<GetPollsResult>(
            sql,
            new { Page = page, PageSize = pageSize, UserId = userId }
        );

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