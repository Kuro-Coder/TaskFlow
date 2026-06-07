
namespace BuildingBlocks.Domain.Abstractions;

public interface IAuditable
{
    Guid? CreatedBy { get; set; }
    DateTime CreatedOnUtc { get; set; }

    Guid? ModifiedBy { get; set; }
    DateTime? ModifiedOnUtc { get; set; }
}