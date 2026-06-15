using Identity.Application.Abstractions;
using Identity.Domain.Repositories;

namespace Identity.Application.Users.Commands.Login;

public sealed class LoginUserCommandHandler
    : ICommandHandler<LoginUserCommand, LoginResponse>
{
    private readonly IUserRepository _repository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHashingService _passwordHasher;

    public LoginUserCommandHandler(
        IUserRepository repository,
        IPasswordHashingService passwordHasher,
        IJwtProvider jwtProvider)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<LoginResponse>> Handle(
        LoginUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmailAsync(
            command.Email, cancellationToken);
        if (user is null)
        {
            return Result<LoginResponse>.Failure(
                new Error(
                    "Identity.InvalidCredentials",
                    "Invalid email or password",
                    ErrorType.Validation));
        }

        var isValid = _passwordHasher.Verify(command.Password, user.PasswordHash);
        if (!isValid)
        {
            return Result<LoginResponse>.Failure(
                new Error(
                    "Identity.InvalidCredentials",
                    "Invalid email or password",
                    ErrorType.Validation));
        }

        var token = _jwtProvider.GenerateToken(user);

        return Result<LoginResponse>.Success(new LoginResponse(
            token.AccessToken,
            token.ExpiresAtUtc));
    }
}