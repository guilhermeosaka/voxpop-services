using Dapper;
using Voxpop.Core.Application.Polls.Dtos;
using Voxpop.Core.Application.Polls.Queries;
using Voxpop.Core.Infrastructure.Persistence.Common.Dapper;
using Voxpop.Core.Infrastructure.Persistence.Polls.Queries.Results;

namespace Voxpop.Core.Infrastructure.Persistence.Polls.Queries;

public class PollQueries(ISqlConnectionFactory connectionFactory) : IPollQueries
{
    public async Task<IReadOnlyList<PollDto>> GetPollsAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var db = connectionFactory.CreateConnection();

        const string sql = """
                           WITH paged_polls AS (
                               SELECT p.id, p.question, p.expires_at, p.created_at
                               FROM polls p
                               WHERE not p.is_archived
                               ORDER BY p.created_at DESC
                               LIMIT @PageSize
                               OFFSET (@Page - 1) * @PageSize
                           )
                           SELECT
                               pp.id          AS "Id",
                               pp.question    AS "Question",
                               pp.expires_at  AS "ExpiresAt",
                               po.id          AS "OptionId",
                               po.value       AS "OptionValue",
                               pp.created_at  AS "CreatedAt",
                           FROM paged_polls pp
                           JOIN poll_options po ON po.poll_id = pp.id
                           ORDER BY pp.created_at DESC;
                           """;

        var result = await db.QueryAsync<GetPollsResult>(
            sql,
            new { Page = page, PageSize = pageSize }
        );

        var lookup = new Dictionary<Guid, PollDto>();

        foreach (var poll in result)
        {
            if (!lookup.TryGetValue(poll.Id, out var pollDto))
            {
                pollDto = new PollDto(poll.Id, poll.Question, poll.ExpiresAt, [], poll.CreatedAt);
                lookup.Add(poll.Id, pollDto);
            }
            
            lookup[poll.Id].Options.Add(new PollOptionDto(poll.OptionId, poll.OptionValue));
        }

        return lookup.Values.ToList();
    }
}