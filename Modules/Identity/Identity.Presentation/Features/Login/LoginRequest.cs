
namespace Identity.Presentation.Features.Login;

public sealed record LoginRequest(
    string Email,
    string Password);