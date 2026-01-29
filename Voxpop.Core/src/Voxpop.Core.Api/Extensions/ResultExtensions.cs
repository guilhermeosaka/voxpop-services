using System.Net;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Packages.Dispatcher.Extensions;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Core.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult(this Result result)
    {
        return result.IsSuccess ? new OkResult() : GetProblemDetails(result.Error);
    }
    
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        return result.IsSuccess ? new OkObjectResult(result.Value) : GetProblemDetails(result.Error);
    }

    private static IActionResult GetProblemDetails(Error? error)
    {
        return error?.Code switch
        {
            _ => new Error(string.Empty, "InternalServerError", "An unexpected error occurred")
                .ToProblemDetails(HttpStatusCode.InternalServerError)
        };
    }
}