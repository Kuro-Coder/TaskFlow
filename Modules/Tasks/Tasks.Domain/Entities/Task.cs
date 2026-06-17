using BuildingBlocks.Domain.Abstractions;
using Tasks.Domain.Entities.Enums;

namespace Tasks.Domain.Entities;

public sealed class TaskItem : AuditableAggregateRoot<Guid>, ISoftDelete
{
    public Guid ProjectId { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public Enums.TaskStatus Status { get; private set; }

    public TaskPriority Priority { get; private set; }

    public DateTime? DueDateUtc { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime? DeletedOnUtc { get; private set; }
}