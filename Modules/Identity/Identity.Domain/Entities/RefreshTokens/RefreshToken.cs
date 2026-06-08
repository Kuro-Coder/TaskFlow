
using BuildingBlocks.Domain.Abstractions;

namespace Identity.Domain.Entities.RefreshTokens;

public sealed class RefreshToken : AuditableAggregateRoot<Guid>, ISoftDelete
{

    public string Token { get; private set; }

    public DateTime ExpiresOnUtc { get; private set; }

    public bool IsRevoked { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime? DeletedOnUtc { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private RefreshToken()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}