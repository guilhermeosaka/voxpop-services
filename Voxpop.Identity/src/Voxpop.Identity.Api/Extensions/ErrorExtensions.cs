using System.Net;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Packages.Handler.Types;

namespace Voxpop.Identity.Api.Extensions;

public static class ErrorExtensions
{
    public static IActionResult ToProblemDetails(this Error error, HttpStatusCode statusCode) =>
        new ObjectResult(new ProblemDetails
        {
            Status = (int)statusCode,
            Title = error.Title,
            Detail = error.Message
        })
        {
            StatusCode = (int)statusCode
        };
}