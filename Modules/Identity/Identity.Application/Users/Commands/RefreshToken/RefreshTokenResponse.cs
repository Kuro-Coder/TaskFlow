namespace Identity.Application.Users.Commands.RefreshToken;

public sealed record RefreshTokenResponse(
    string AccessToken,
    DateTime ExpiresAtUtc,
    string RefreshToken);
