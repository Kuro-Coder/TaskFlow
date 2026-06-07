using Identity.Domain.Entities.Users;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly IdentityDbContext _context;
    public UserRepository(
        IdentityDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(
            user,
            cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(
        string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Email == email,
                cancellationToken);
    }

    public async Task<User?> GetByIdAsync(
        Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }
}
