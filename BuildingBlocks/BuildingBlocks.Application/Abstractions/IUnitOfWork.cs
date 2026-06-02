
namespace BuildingBlocks.Application.Abstractions;

public interface IUnitOfWork
{
    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}