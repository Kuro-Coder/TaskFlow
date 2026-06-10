using BuildingBlocks.Application.Messaging;

namespace Identity.Application.Users.Commands.Register;

public sealed record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password)
    : ICommand<Guid>;