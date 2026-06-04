using BuildingBlocks.Domain.Abstractions;

namespace BuildingBlocks.Application.Messaging;

public interface IDomainEventHandler<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    Task Handle(
        TDomainEvent domainEvent,
        CancellationToken cancellationToken);
}