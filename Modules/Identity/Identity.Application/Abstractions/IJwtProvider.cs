
using Identity.Domain.Entities.Users;

namespace Identity.Application.Abstractions;

public interface IJwtProvider
{
    string Generate(
        User user);
}