
namespace Identity.Application.Users;

public sealed record AccessTokenResponse(
    string AccessToken,
    DateTime ExpiresAtUtc);
