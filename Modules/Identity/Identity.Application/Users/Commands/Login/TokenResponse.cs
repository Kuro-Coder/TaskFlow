namespace Identity.Application.Users.Commands.Login;

public sealed record TokenResponse(
    string AccessToken,
    DateTime ExpiresAtUtc);