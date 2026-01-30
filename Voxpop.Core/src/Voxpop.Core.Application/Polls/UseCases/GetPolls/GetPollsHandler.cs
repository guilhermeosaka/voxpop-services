using Voxpop.Core.Application.Polls.Dtos;
using Voxpop.Core.Application.Polls.Queries;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Polls.UseCases.GetPolls;

public class GetPollsHandler(IPollQueries queries) : IHandler<GetPollsQuery, IReadOnlyList<PollDto>>
{
    public async Task<Result<IReadOnlyList<PollDto>>> Handle(GetPollsQuery query, CancellationToken ct)
    {
        var result = await queries.GetPollsAsync(query.Page, query.PageSize, ct);
        return result.ToList();
    }
}