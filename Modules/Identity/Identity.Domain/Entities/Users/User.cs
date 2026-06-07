using BuildingBlocks.Domain.Abstractions;

namespace Identity.Domain.Entities.Users;

public sealed class User
    : AuditableAggregateRoot<Guid>
{
    private User()
    {
    }

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

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public bool IsActive { get; private set; }

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
}