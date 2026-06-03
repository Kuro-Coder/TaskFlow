using BuildingBlocks.Application.Abstractions;
using BuildingBlocks.Application.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projects.Application.Commands.Create;
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

        // Handlers
        services.Scan(scan => scan
            .FromAssemblies(typeof(CreateProjectCommandHandler).Assembly)
            .AddClasses(classes =>
                classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}