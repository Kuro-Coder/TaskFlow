namespace Identity.Application.Users.Commands.Login;

public sealed record LoginResponse(
    string AccessToken,
    DateTime ExpiresAtUtc);