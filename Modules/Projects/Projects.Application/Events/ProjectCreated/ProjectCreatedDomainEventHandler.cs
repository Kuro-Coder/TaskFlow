using BuildingBlocks.Application.Messaging;
using Projects.Domain.Events;
using System.Diagnostics;

namespace Projects.Application.Events.ProjectCreated;

public sealed class ProjectCreatedDomainEventHandler
    : IDomainEventHandler<ProjectCreatedDomainEvent>
{
    public Task Handle(
        ProjectCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        Debug.WriteLine($"Project created: {domainEvent.ProjectId}");

        return Task.CompletedTask;
    }
}