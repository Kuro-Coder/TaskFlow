using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BuildingBlocks.Application.Messaging;
using Identity.Application.Users.Commands;
using BuildingBlocks.Presentation.Extensions;

namespace Identity.Presentation.Features.Register;

[ApiController]
[Route("api/identity/register")]
[Tags("Identity")]
public sealed class RegisterEndpoint : ControllerBase
{
    private readonly ICommandDispatcher _dispatcher;

    public RegisterEndpoint(
        ICommandDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
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