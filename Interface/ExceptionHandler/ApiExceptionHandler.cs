using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LostColonyManager.Interface.ExceptionHandler
{
    public sealed class ApiExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken
        )
        {
            var (statusCode, title, detail) = exception switch
            {
                ArgumentNullException ex => (
                    StatusCodes.Status400BadRequest,
                    "Invalid request",
                    ex.Message
                ),

                ArgumentException ex => (
                    StatusCodes.Status400BadRequest,
                    "Validation error",
                    ex.Message
                ),

                InvalidOperationException ex => (
                    StatusCodes.Status409Conflict,
                    "Conflict",
                    ex.Message
                ),

                KeyNotFoundException ex => (
                    StatusCodes.Status404NotFound,
                    "Not found",
                    ex.Message
                ),

                UnauthorizedAccessException ex => (
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized",
                    ex.Message
                ),

                _ => (
                    StatusCodes.Status500InternalServerError,
                    "Unexpected error",
                    "An unexpected error occurred."
                )
            };

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path
            };

            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

            return true;
        }
    }
}
