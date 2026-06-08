using BuildingBlocks.Application.Abstractions;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Results;
using Identity.Application.Abstractions;
using Identity.Domain.Entities.Users;
using Identity.Domain.Repositories;

namespace Identity.Application.Users.Commands;

public sealed class RegisterUserCommandHandler
    : ICommandHandler<
        RegisterUserCommand,
        Guid>
{
    private readonly IUserRepository _repository;
    private readonly IPasswordHashingService _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(
        IUserRepository repository,
        IPasswordHashingService passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var exists =
            await _repository.GetByEmailAsync(
                command.Email,
                cancellationToken);

        if (exists is not null)
        {
            return Result<Guid>.Failure(
                new Error(
                    "Identity.EmailAlreadyExists",
                    "Email already exists"));
        }

        var user = User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            _passwordHasher.Hash(
                command.Password));

        await _repository.AddAsync(
            user,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Result<Guid>.Success(
            user.Id);
    }
}