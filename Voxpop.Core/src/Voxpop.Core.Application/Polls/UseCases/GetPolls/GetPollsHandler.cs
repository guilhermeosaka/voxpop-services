using Voxpop.Core.Application.Common.Interfaces;
using Voxpop.Core.Application.Polls.Models;
using Voxpop.Core.Application.Polls.Queries;
using Voxpop.Packages.Dispatcher.Interfaces;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Application.Polls.UseCases.GetPolls;
 
 public class GetPollsHandler(IPollQueries queries, IRequestContext requestContext) : IHandler<GetPollsQuery, IReadOnlyList<PollSummary>>
 {
     public async Task<Result<IReadOnlyList<PollSummary>>> Handle(GetPollsQuery query, CancellationToken ct)
     {
         var result = await queries.GetPollsAsync(query.Page, query.PageSize, requestContext.UserId, ct);
         return result.ToList();
     }
 }