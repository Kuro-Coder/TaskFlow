using Identity.Application.Abstractions;
using Identity.Domain.Repositories;

namespace Identity.Application.Users.Commands.RefreshToken;

public sealed class RefreshTokenCommandHandler
    : ICommandHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RefreshTokenResponse>> Handle(
        RefreshTokenCommand command,
        CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(
            command.RefreshToken, cancellationToken);

        if (refreshToken is null)
            return Result<RefreshTokenResponse>.Failure(
                new Error("RefreshToken.NotFound", "RefreshToken not found",
                    ErrorType.NotFound));

        if (refreshToken.IsRevoked)
            return Result<RefreshTokenResponse>.Failure(
                new Error("IsRevoked.Invalid", "Invalid Refresh Token",
                    ErrorType.Validation));

        if (refreshToken.IsExpired)
            return Result<RefreshTokenResponse>.Failure(
                new Error("IsExpired.Invalid", "Is Expired",
                    ErrorType.Validation));

        var user = await _userRepository.GetByIdAsync(refreshToken.UserId, cancellationToken);
        if (user is null)
            return Result<RefreshTokenResponse>.Failure(
                new Error("UserNotFound.NotFound", "User not found",
                    ErrorType.NotFound));

        var accessToken = _jwtProvider.GenerateToken(user);
        var refreshTokenValue = _jwtProvider.GenerateRefreshToken();

        refreshToken.Revoke();

        var newRefreshToken = Domain.Entities.RefreshTokens.RefreshToken.Create(
            user.Id,
            refreshTokenValue,
            DateTime.UtcNow.AddDays(30));

        await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<RefreshTokenResponse>.Success(new RefreshTokenResponse(
            accessToken.AccessToken,
            accessToken.ExpiresAtUtc,
            refreshTokenValue));
    }
}