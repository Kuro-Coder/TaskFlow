using BuildingBlocks.Application.Abstractions;
using BuildingBlocks.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuildingBlocks.Infrastructure.Persistence.Interceptors;

public sealed class AuditInterceptor
    : SaveChangesInterceptor
{
    private readonly ICurrentUser _currentUser;
    public AuditInterceptor(ICurrentUser currentUser)
    {
        _currentUser = currentUser;
    }

    public override ValueTask<InterceptionResult<int>>
        SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context is null)
            return base.SavingChangesAsync(
                eventData,
                result,
                cancellationToken);

        var entries =
            context.ChangeTracker
            .Entries<IAuditable>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedBy = _currentUser.UserId;
                entry.Entity.CreatedOnUtc = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(nameof(IAuditable.CreatedOnUtc))
                    .IsModified = false;

                entry.Property(nameof(IAuditable.CreatedBy))
                    .IsModified = false;

                entry.Entity.ModifiedBy = _currentUser.UserId;
                entry.Entity.ModifiedOnUtc = DateTime.UtcNow;
            }
        }

        return base.SavingChangesAsync(
            eventData,
            result,
            cancellationToken);
    }
}