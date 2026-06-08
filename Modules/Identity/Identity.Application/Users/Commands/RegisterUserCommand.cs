using BuildingBlocks.Application.Messaging;

namespace Identity.Application.Users.Commands;

public sealed record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password)
    : ICommand<Guid>;