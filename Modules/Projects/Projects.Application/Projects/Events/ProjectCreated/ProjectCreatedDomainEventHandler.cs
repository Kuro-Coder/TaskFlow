using BuildingBlocks.Application.Messaging;
using Projects.Domain.Events;

namespace Projects.Application.Projects.Events.ProjectCreated;

public sealed class ProjectCreatedDomainEventHandler
    : IDomainEventHandler<ProjectCreatedDomainEvent>
{
    public Task Handle(
        ProjectCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        Console.WriteLine($"Project created: {domainEvent.ProjectId}");

        return Task.CompletedTask;
    }
}