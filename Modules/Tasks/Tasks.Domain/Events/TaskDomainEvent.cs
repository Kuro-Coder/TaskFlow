using BuildingBlocks.Domain.Abstractions;

namespace Tasks.Domain.Events;

public sealed record TaskCreatedDomainEvent(
    Guid TaskId) : DomainEvent;

public sealed record TaskCompletedDomainEvent(
    Guid TaskId) : DomainEvent;

public sealed record TaskCancelledDomainEvent(
    Guid TaskId) : DomainEvent;
