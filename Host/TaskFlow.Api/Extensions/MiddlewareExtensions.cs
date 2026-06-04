using TaskFlow.Api.Middleware;

namespace TaskFlow.Api.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder
        UseGlobalExceptionHandler(
            this IApplicationBuilder app)
    {
        return app.UseMiddleware<
            ExceptionHandlingMiddleware>();
    }
}