using BuildingBlocks.Domain.Abstractions;
using Tasks.Domain.Entities.Enums;
using Tasks.Domain.Events;

namespace Tasks.Domain.Entities;

public sealed class TaskItem
    : AuditableAggregateRoot<Guid>, ISoftDelete
{
    public Guid ProjectId { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public TaskItemStatus Status { get; private set; }

    public TaskPriority Priority { get; private set; }

    public DateTime? DueDateUtc { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime? DeletedOnUtc { get; private set; }

    private TaskItem(
        Guid id,
        Guid projectId,
        string title,
        string? description,
        TaskPriority priority,
        DateTime? dueDateUtc)
        : base(id)
    {
        ProjectId = projectId;
        Title = title;
        Description = description;
        Priority = priority;
        DueDateUtc = dueDateUtc;
        Status = TaskItemStatus.Todo;
    }

    public static TaskItem Create(
        Guid projectId,
        string title,
        string? description,
        TaskPriority priority,
        DateTime? dueDateUtc)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException(
                "Task title is required.");

        var taskItem = new TaskItem(
            Guid.NewGuid(),
            projectId,
            title,
            description,
            priority,
            dueDateUtc);


        taskItem.Raise(
            new TaskCreatedDomainEvent(
                taskItem.Id));

        return taskItem;
    }

    public void Update(
        string title,
        string? description,
        TaskPriority priority,
        DateTime? dueDateUtc)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException(
                "Task title is required.");

        Title = title;
        Description = description;
        Priority = priority;
        DueDateUtc = dueDateUtc;
    }

    public void Start()
    {
        if (Status != TaskItemStatus.Todo)
            throw new DomainException(
                "Only Todo tasks can be started.");

        Status = TaskItemStatus.InProgress;
    }

    public void Complete()
    {
        if (Status == TaskItemStatus.Completed)
            return;

        if (Status == TaskItemStatus.Cancelled)
            throw new DomainException(
                "Cancelled task cannot be completed.");

        Status = TaskItemStatus.Completed;
    }

    public void Cancel()
    {
        if (Status == TaskItemStatus.Completed)
            throw new DomainException(
                "Completed task cannot be cancelled.");

        Status = TaskItemStatus.Cancelled;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedOnUtc = DateTime.UtcNow;
    }

    private TaskItem()
    {
    }
}