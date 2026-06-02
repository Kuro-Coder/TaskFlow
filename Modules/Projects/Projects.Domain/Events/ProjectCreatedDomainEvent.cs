using BuildingBlocks.Domain.Abstractions;

namespace Projects.Domain.Events;

public sealed record ProjectCreatedDomainEvent(
    Guid ProjectId)
    : IDomainEvent;
