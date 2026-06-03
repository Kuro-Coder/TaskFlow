using Microsoft.EntityFrameworkCore;
using Projects.Domain.Entities;
using Projects.Domain.Repositories;
using Projects.Infrastructure.Persistence;

namespace Projects.Infrastructure.Repositories;

public sealed class ProjectRepository
    : IProjectRepository
{
    private readonly ProjectsDbContext _context;

    public ProjectRepository(
        ProjectsDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Project project,
        CancellationToken cancellationToken)
    {
        await _context.Projects.AddAsync(
            project,
            cancellationToken);
    }

    public async Task<Project?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Projects
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }
    public async Task<bool> ExistsAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Projects
            .AnyAsync(
                x => x.Id == id,
                cancellationToken);
    }
}