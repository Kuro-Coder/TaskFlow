using BuildingBlocks.Application.Abstractions;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Infrastructure.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projects.Domain.Repositories;
using Projects.Infrastructure.Persistence;
using Projects.Infrastructure.Repositories;

namespace Projects.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddProjectsInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<ProjectsDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IProjectRepository, ProjectRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();

        // Handlers
        services.Scan(scan => scan
            .FromAssemblies(
                typeof(Projects.Application.AssemblyReference).Assembly)
            .AddClasses(c =>
                c.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            .AddClasses(c =>
                c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}