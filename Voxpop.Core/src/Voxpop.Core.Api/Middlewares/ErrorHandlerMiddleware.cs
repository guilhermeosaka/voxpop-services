using System.Net;
using Microsoft.AspNetCore.Mvc;
using Voxpop.Core.Domain.Common.Exceptions;

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
            var problemDetails = GetProblemDetails(ex);

            if (ex is DomainException)
                logger.LogWarning(ex, "Domain exception occurred");
            else
                logger.LogError(ex, "Unhandled exception occurred");
            
            context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
    
    private static ProblemDetails GetProblemDetails(Exception exception) =>
        exception switch
        {
            ConflictException ex => new ProblemDetails
            {
                Title = ex.Title,
                Detail = ex.Message,
                Status = StatusCodes.Status409Conflict
            },

            _ => new ProblemDetails
            {
                Title = "Unexpected error",
                Detail = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError
            }
        };
}