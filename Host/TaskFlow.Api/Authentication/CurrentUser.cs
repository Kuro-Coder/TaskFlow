using BuildingBlocks.Application.Abstractions;
using System.Security.Claims;

namespace TaskFlow.Api.Authentication;

internal sealed class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var value = _httpContextAccessor
                .HttpContext?
                .User
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            return Guid.TryParse(value, out var id)
                ? id
                : null;
        }
    }

    public string? UserName =>
        _httpContextAccessor.HttpContext?
            .User
            .FindFirst(ClaimTypes.Name)?
            .Value;

    public string? Email =>
        _httpContextAccessor.HttpContext?
            .User
            .FindFirst(ClaimTypes.Email)?
            .Value;

}