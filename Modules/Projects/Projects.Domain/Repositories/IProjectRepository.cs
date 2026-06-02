using Projects.Domain.Entities;

namespace Projects.Domain.Repositories;

public interface IProjectRepository
{
    Task AddAsync(
        Project project,
        CancellationToken cancellationToken);

    Task<Project?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);
}
