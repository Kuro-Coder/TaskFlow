using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Results;
using Identity.Application.Abstractions;
using Identity.Domain.Repositories;

namespace Identity.Application.Users.Commands.Login;

public sealed class LoginUserCommandHandler
    : ICommandHandler<
        LoginUserCommand,
        LoginResponse>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHashingService _passwordHasher;

    public LoginUserCommandHandler(
        IUserRepository repository,
        IPasswordHashingService passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<LoginResponse>> Handle(
        LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        var user =
            await _repository.GetByEmailAsync(
                command.Email,
                cancellationToken);

        if (user is null)
        {
            return Result<LoginResponse>.Failure(
                new Error(
                    "Identity.InvalidCredentials",
                    "Invalid email or password",
                    ErrorType.Validation));
        }

        var isValid =
            _passwordHasher.Verify(
                command.Password,
                user.PasswordHash);

        if (!isValid)
        {
            return Result<LoginResponse>.Failure(
                new Error(
                    "Identity.InvalidCredentials",
                    "Invalid email or password",
                    ErrorType.Validation));
        }

        throw new NotImplementedException();
    }
}