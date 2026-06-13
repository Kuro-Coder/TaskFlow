using BuildingBlocks.Domain.Abstractions;
using Identity.Domain.Entities.RefreshTokens;

namespace Identity.Domain.Entities.Users;

public sealed class User : AuditableAggregateRoot<Guid>, ISoftDelete
{
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTime? DeletedOnUtc { get; private set; }

    private User(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string passwordHash)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        IsActive = true;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string passwordHash)
    {
        return new User(
            Guid.NewGuid(),
            firstName,
            lastName,
            email,
            passwordHash);
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedOnUtc = DateTime.UtcNow;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private readonly List<RefreshToken> _refreshTokens = [];
    public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;

    private User()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }

}