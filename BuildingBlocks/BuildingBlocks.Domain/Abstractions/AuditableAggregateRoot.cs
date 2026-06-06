
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

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

}