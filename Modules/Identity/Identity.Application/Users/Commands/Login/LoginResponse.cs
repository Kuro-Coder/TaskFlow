using BuildingBlocks.Application.Messaging;

namespace Identity.Application.Users.Commands.Login;

public sealed record LoginResponse(
    string AccessToken,
    string RefreshToken);