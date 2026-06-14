using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Presentation.Extensions;
using Identity.Application.Users.Commands.Login;

namespace Identity.Presentation.Features.Login;

[ApiController]
[Route("api/identity/user")]
[Tags("Identity")]
public sealed class LoginEndpoint : ControllerBase
{
    private readonly ICommandDispatcher _dispatcher;

    public LoginEndpoint(
        ICommandDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost("login")]
    public async Task<IResult> Handle(
        LoginRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Dispatch(new LoginUserCommand(
            request.Email,
            request.Password),
            cancellationToken);
        if (result.IsFailure)
            return result.ToProblemResult();

        return Results.Ok(result.Value);
    }
}