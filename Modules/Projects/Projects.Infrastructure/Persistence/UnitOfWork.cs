using BuildingBlocks.Application.Abstractions;

namespace Projects.Infrastructure.Persistence;

public sealed class UnitOfWork
    : IUnitOfWork
{
    private readonly ProjectsDbContext _context;

    public UnitOfWork(
        ProjectsDbContext context)
    {
        _context = context;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(
            cancellationToken);
    }
}