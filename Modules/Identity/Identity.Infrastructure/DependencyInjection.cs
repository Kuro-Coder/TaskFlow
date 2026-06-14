using BuildingBlocks.Application.Abstractions;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Infrastructure.Messaging;
using BuildingBlocks.Infrastructure.Persistence.Interceptors;
using Identity.Application.Abstractions;
using Identity.Application.Users;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Authentication;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;


public static class DependencyInjection
{
    public static IServiceCollection AddIdentitiesInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<IdentityDbContext>((sp, options) =>
        {
            options.UseSqlServer(connectionString);

            options.AddInterceptors(
                sp.GetRequiredService<AuditInterceptor>(),
                sp.GetRequiredService<DomainEventsInterceptor>());
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //Repos
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        services.AddScoped<IPasswordHashingService, PasswordHashingService>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        // Handlers
        services.Scan(scan => scan
            .FromAssemblies(typeof(Identity.Application.AssemblyReference).Assembly)
            .AddClasses(c =>
                c.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            .AddClasses(c =>
                c.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(
                typeof(Identity.Application.AssemblyReference).Assembly)
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