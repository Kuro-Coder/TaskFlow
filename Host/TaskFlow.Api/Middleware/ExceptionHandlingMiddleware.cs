using BuildingBlocks.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace TaskFlow.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception,
                exception.Message);

            await HandleExceptionAsync(
                context,
                exception);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        ProblemDetails problem;
        switch (exception)
        {
            case NotFoundException ex:

                problem = new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = ex.Message,
                    Status = StatusCodes.Status404NotFound
                };

                break;

            case ConflictException ex:

                problem = new ProblemDetails
                {
                    Title = "Conflict",
                    Detail = ex.Message,
                    Status = StatusCodes.Status409Conflict
                };

                break;

            case ValidationException ex:

                problem = new ValidationProblemDetails(ex.Errors)
                {
                    Title = "Validation Error",
                    Status = StatusCodes.Status400BadRequest
                };

                break;

            default:

                problem = new ProblemDetails
                {
                    Title = "Server Error",
                    Detail = exception.Message,
                    Status = StatusCodes.Status500InternalServerError
                };

                break;
        }


        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problem.Status.Value;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(problem));
    }
}