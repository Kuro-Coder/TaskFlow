using BuildingBlocks.Application.Messaging;

namespace Identity.Application.Users.Commands.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken)
    : ICommand<RefreshTokenResponse>;
