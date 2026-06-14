using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Presentation.Extensions;
using Identity.Application.Users.Commands.Register;

namespace Identity.Presentation.Features.Register;

[ApiController]
[Route("api/identity/user")]
[Tags("Identity")]
public sealed class RegisterEndpoint : ControllerBase
{
    private readonly ICommandDispatcher _dispatcher;

    public RegisterEndpoint(
        ICommandDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost("register")]
    public async Task<IResult> Handle(
        RegisterRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Dispatch(new RegisterUserCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password),
            cancellationToken);
        if (result.IsFailure)
        {
            return result.ToProblemResult();
        }

        return Results.Ok(result.Value);
    }
}