using BuildingBlocks.Domain.Abstractions;
using Projects.Domain.Events;

namespace Projects.Domain.Entities;

public sealed class Project : AuditableAggregateRoot<Guid>, ISoftDelete
{
    public string Name { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime? DeletedOnUtc { get; private set; }

    private Project(
        Guid id,
        string name)
        : base(id)
    {
        Name = name;
    }

    public static Project Create(
        string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(
                "Project name is required.");

        var project = new Project(
            Guid.NewGuid(),
            name);

        project.Raise(
            new ProjectCreatedDomainEvent(
                project.Id));

        return project;
    }

    public void UpdateName(
    string name)
    {
        Name = name;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedOnUtc = DateTime.UtcNow;
    }

    private Project()
    {
    }

}