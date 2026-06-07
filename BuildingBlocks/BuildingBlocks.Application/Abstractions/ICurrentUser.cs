
namespace BuildingBlocks.Application.Abstractions;

public interface ICurrentUser
{
    Guid? UserId { get; }
    string? UserName { get; }
    string? Email { get; }
}