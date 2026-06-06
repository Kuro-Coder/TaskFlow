using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

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
        context.Response.ContentType =
            "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Title = "Server Error",
            Detail = exception.Message,
            Status = (int)HttpStatusCode.InternalServerError
        };

        context.Response.StatusCode =
            problemDetails.Status.Value;

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(problemDetails));
    }
}