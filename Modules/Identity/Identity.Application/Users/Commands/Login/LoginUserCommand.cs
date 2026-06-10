using BuildingBlocks.Application.Messaging;

namespace Identity.Application.Users.Commands.Login;

public sealed record LoginUserCommand(
    string Email,
    string Password)
    : ICommand<LoginResponse>;