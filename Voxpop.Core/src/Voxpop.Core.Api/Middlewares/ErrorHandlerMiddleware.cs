using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Voxpop.Core.Api.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var problemDetails = new ProblemDetails
            {
                Title = "Unexpected error",
                Detail = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError
            };

            logger.LogError(ex, "Unhandled exception occurred");
            
            context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}