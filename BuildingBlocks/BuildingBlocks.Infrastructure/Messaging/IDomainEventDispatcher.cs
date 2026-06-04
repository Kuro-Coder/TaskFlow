using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.Infrastructure.Messaging;

public interface IDomainEventDispatcher
{
    Task Dispatch(
        IDomainEvent domainEvent,
        CancellationToken cancellationToken);
}