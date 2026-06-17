using Microsoft.Extensions.DependencyInjection;

namespace Tasks.Presentation;


public static class DependencyInjection
{
    public static IServiceCollection AddTasksPresentation(
        this IServiceCollection services)
    {
        return services;
    }
}
