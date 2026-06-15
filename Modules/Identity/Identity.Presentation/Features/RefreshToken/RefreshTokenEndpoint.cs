using Identity.Application.Users.Commands.RefreshToken;

namespace Identity.Presentation.Features.RefreshToken;

[ApiController]
[Route("api/identity/user")]
[Tags("Identity")]
public sealed class RefreshTokenEndpoint : ControllerBase
{
    private readonly ICommandDispatcher _dispatcher;

    public RefreshTokenEndpoint(
        ICommandDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost("refresh-token")]
    public async Task<IResult> Handle(
        RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Dispatch(new RefreshTokenCommand(
            request.RefreshToken),
            cancellationToken);
        if (result.IsFailure)
            return result.ToProblemResult();

        return Results.Ok(result.Value);
    }
}