using BuildingBlocks.Domain.Abstractions;
using BuildingBlocks.Infrastructure.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BuildingBlocks.Infrastructure.Persistence.Interceptors;

public sealed class DomainEventsInterceptor
    : SaveChangesInterceptor
{
    private readonly IDomainEventDispatcher _dispatcher;

    public DomainEventsInterceptor(
        IDomainEventDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public override async ValueTask<int>
        SavedChangesAsync(
            SaveChangesCompletedEventData eventData,
            int result,
            CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context is null)
        {
            return await base.SavedChangesAsync(
                eventData,
                result,
                cancellationToken);
        }

        var aggregates = context.ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = aggregates
            .SelectMany(x => x.DomainEvents)
            .ToList();

        foreach (var aggregate in aggregates)
        {
            aggregate.ClearDomainEvents();
        }

        foreach (var domainEvent in domainEvents)
        {
            await _dispatcher.Dispatch(
                domainEvent,
                cancellationToken);
        }

        return await base.SavedChangesAsync(
            eventData,
            result,
            cancellationToken);
    }
}