using Microsoft.Extensions.DependencyInjection;

namespace Identity.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityPresentation(
        this IServiceCollection services)
    {
        return services;
    }
}
