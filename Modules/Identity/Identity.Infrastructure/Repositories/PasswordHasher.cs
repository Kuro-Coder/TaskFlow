using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Repositories;


public sealed class PasswordHashingService : IPasswordHashingService
{
    private readonly PasswordHasher<object> _passwordHasher = new();

    public string Hash(
        string password)
    {
        return _passwordHasher.HashPassword(
            null!,
            password);
    }

    public bool Verify(
        string password,
        string passwordHash)
    {
        var result =
            _passwordHasher.VerifyHashedPassword(
                null!,
                passwordHash,
                password);

        return result
            != PasswordVerificationResult.Failed;
    }
}