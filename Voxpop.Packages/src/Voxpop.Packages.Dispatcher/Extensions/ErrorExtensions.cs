using System.Net;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Packages.Dispatcher.Types;

namespace Voxpop.Packages.Dispatcher.Extensions;

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