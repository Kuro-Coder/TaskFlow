using Microsoft.Extensions.DependencyInjection;

namespace Projects.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddProjectsPresentation(
        this IServiceCollection services)
    {
        return services;
    }
}
