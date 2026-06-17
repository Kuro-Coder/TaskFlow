using BuildingBlocks.Application.Abstractions;

namespace Tasks.Infrastructure.Persistence;

public sealed class UnitOfWork
    : IUnitOfWork
{
    private readonly TasksDbContext _context;

    public UnitOfWork(
        TasksDbContext context)
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