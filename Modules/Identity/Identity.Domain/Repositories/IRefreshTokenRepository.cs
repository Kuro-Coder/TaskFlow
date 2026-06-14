using Identity.Domain.Entities.RefreshTokens;

namespace Identity.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task AddAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken);

    Task<RefreshToken?> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken);
}