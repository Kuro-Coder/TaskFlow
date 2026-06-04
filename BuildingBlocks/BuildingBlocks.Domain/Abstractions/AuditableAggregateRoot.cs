
namespace BuildingBlocks.Domain.Abstractions;

public abstract class AuditableAggregateRoot<TId>
    : AggregateRoot<TId>
{
    protected AuditableAggregateRoot()
    {
    }

    protected AuditableAggregateRoot(TId id)
        : base(id)
    {
    }

    public DateTime CreatedOnUtc { get; protected set; }

    public DateTime? ModifiedOnUtc { get; protected set; }

}