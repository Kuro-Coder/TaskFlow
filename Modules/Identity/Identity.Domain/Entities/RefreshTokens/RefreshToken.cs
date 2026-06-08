
namespace Identity.Domain.Entities.RefreshTokens;

public sealed class RefreshToken
{
    public Guid Id { get; private set; }

    public string Token { get; private set; }

    public DateTime ExpiresOnUtc { get; private set; }

    public bool IsRevoked { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private RefreshToken()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }
}