
namespace BuildingBlocks.Domain.Abstractions;

public abstract class AuditableAggregateRoot<TId>
    : AggregateRoot<TId>,
      IAuditable
{
    protected AuditableAggregateRoot()
    {
    }

    protected AuditableAggregateRoot(TId id)
        : base(id)
    {
    }

    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }

    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

}