using Identity.Domain.Entities.Users;

namespace Identity.Domain.Entities.RefreshTokens;

public sealed class RefreshToken : AuditableAggregateRoot<Guid>, ISoftDelete
{
    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public string Token { get; private set; }

    public string? ReplacedByToken { get; private set; }

    public DateTime ExpiresOnUtc { get; private set; }

    public DateTime? RevokedOnUtc { get; private set; }

    public bool IsDeleted { get; private set; }
    public DateTime? DeletedOnUtc { get; private set; }

    public bool IsRevoked =>
        RevokedOnUtc.HasValue;

    public bool IsExpired =>
        DateTime.UtcNow >= ExpiresOnUtc;

    private RefreshToken(
        Guid id,
        Guid userId,
        string token,
        DateTime expiresOnUtc) : base(id)
    {
        UserId = userId;
        Token = token;
        ExpiresOnUtc = expiresOnUtc;
    }

    public static RefreshToken Create(
        Guid userId,
        string token,
        DateTime expiresOnUtc)
    {
        return new RefreshToken(
            Guid.NewGuid(),
            userId,
            token,
            expiresOnUtc);
    }

    public void Revoke()
    {
        RevokedOnUtc = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedOnUtc = DateTime.UtcNow;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private RefreshToken()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}