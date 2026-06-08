using Identity.Domain.Entities.Users;

namespace Identity.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken);

    Task AddAsync(
        User user,
        CancellationToken cancellationToken);
}