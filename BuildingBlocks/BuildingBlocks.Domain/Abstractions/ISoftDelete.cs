
namespace BuildingBlocks.Domain.Abstractions;

public interface ISoftDelete
{
    bool IsDeleted { get; }

    DateTime? DeletedOnUtc { get; }
}