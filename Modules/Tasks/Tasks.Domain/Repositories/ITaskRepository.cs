using Tasks.Domain.Entities;

namespace Tasks.Domain.Repositories;

public interface ITaskRepository
{
    Task AddAsync(
        TaskItem task,
        CancellationToken cancellationToken);

    Task<TaskItem?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}