
using Identity.Application.Users.Commands.Login;
using Identity.Domain.Entities.Users;

namespace Identity.Application.Abstractions;

public interface IJwtProvider
{
    TokenResponse GenerateToken(User user);

    string GenerateRefreshToken();
}