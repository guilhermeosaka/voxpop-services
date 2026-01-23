using System.Net;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Identity.Application.Common;
using Voxpop.Packages.Handler.Types;

namespace Voxpop.Identity.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToHttpResult(this Result result)
    {
        return result.IsSuccess ? new OkResult() : GetProblemDetails(result.Error);
    }
    
    public static IActionResult ToHttpResult<T>(this Result<T> result)
    {
        return result.IsSuccess ? new OkObjectResult(result.Value) : GetProblemDetails(result.Error);
    }

    private static IActionResult GetProblemDetails(Error? error)
    {
        return error?.Code switch
        {
            Errors.InvalidCodeCode => error.ToProblemDetails(HttpStatusCode.Unauthorized),
            Errors.UserConflictCode => error.ToProblemDetails(HttpStatusCode.Conflict),
            Errors.UserNotFoundCode => error.ToProblemDetails(HttpStatusCode.NotFound),
            _ => new Error(string.Empty, "InternalServerError", "An unexpected error occurred")
                .ToProblemDetails(HttpStatusCode.InternalServerError)
        };
    }
}