
namespace Identity.Domain.Entities.RefreshTokens;

public sealed class RefreshToken
{
    public Guid Id { get; private set; }

    public string Token { get; private set; }

    public DateTime ExpiresOnUtc { get; private set; }

    public bool IsRevoked { get; private set; }

    private RefreshToken()
    {
    }
}