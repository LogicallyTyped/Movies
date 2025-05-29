using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Movies.Contracts.Exceptions;

namespace Movies.Server.Handlers;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = CreateProblemDetials(exception);

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }

    private static ProblemDetails CreateProblemDetials(Exception exception)
    {
        ProblemDetails problemDetails = exception switch
        {
            NotFoundExecption => CreateProblemDetials(StatusCodes.Status404NotFound, "Not Found", exception.Message),
            CustomValidationException => CreateProblemDetials(StatusCodes.Status400BadRequest, "Validation Error", exception.Message),
            _ => CreateProblemDetials(StatusCodes.Status500InternalServerError, "Internal Server Error", "An unexpected error occurred.")
        };

        if (exception is CustomValidationException customValidationException)
        {
            problemDetails.Extensions["errors"] = customValidationException.ValidationErrors;
        }

        return problemDetails;
    }

    private static ProblemDetails CreateProblemDetials(int status, string title, string detail)
    {
        return new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail
        };
    }
}