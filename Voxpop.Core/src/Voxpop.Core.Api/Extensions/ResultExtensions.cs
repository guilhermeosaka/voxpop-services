using System.Net;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Core.Application.Common;
using Voxpop.Packages.Dispatcher.Extensions;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult(this Result result)
    {
        return result.IsSuccess ? new OkResult() : GetProblemDetails(result.Error);
    }

    public static IActionResult ToActionResult<T, TResponse>(this Result<T> result, Func<T, TResponse> map)
    {
        return result.IsSuccess ? new OkObjectResult(map(result.Value!)) : GetProblemDetails(result.Error);
    }

    public static IActionResult ToCreatedResult<T, TResponse>(
        this Result<T> result, 
        string routeName,
        object routeValues, 
        Func<T, TResponse> map)
    {
        return result.IsSuccess
            ? new CreatedAtRouteResult(routeName, routeValues, map(result.Value!))
            : GetProblemDetails(result.Error);
    }

    private static IActionResult GetProblemDetails(Error? error)
    {
        return error?.Code switch
        {
            Errors.UserNotFoundCode => error.ToProblemDetails(HttpStatusCode.NotFound),
            Errors.PollNotFoundCode => error.ToProblemDetails(HttpStatusCode.NotFound),
            Errors.VoteNotFoundCode => error.ToProblemDetails(HttpStatusCode.NotFound),
            Errors.ReactionNotFoundCode => error.ToProblemDetails(HttpStatusCode.NotFound),
            Errors.UnauthorizedUserCode => error.ToProblemDetails(HttpStatusCode.Unauthorized),
            Errors.PollVotingIsClosedCode => error.ToProblemDetails(HttpStatusCode.Forbidden),
            _ => new Error(string.Empty, "InternalServerError", "An unexpected error occurred")
                .ToProblemDetails(HttpStatusCode.InternalServerError)
        };
    }
}