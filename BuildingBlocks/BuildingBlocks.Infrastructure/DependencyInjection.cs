using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Infrastructure.Messaging;
using BuildingBlocks.Infrastructure.Persistence.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddBuildingBlocks(
        this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();

        services.AddScoped<IQueryDispatcher, QueryDispatcher>();

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        services.AddScoped<AuditInterceptor>();

        services.AddScoped<DomainEventsInterceptor>();

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        return services;
    }
}