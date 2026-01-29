namespace Voxpop.Core.Api.Requests;

public record GetPollsRequest(int Page = 1, int PageSize = 50);