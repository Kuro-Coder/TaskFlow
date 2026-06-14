using BuildingBlocks.Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Presentation.Features.CurentUser;

[ApiController]
[Route("api/identity/user")]
[Tags("Identity")]
public sealed class CurentUserEndpoint : ControllerBase
{
    public CurentUserEndpoint()
    {
    }

    [HttpGet("me")]
    [Authorize]
    public IResult Me(
        [FromServices] ICurrentUser currentUser)
    {
        return Results.Ok(
            new
            {
                currentUser.UserId
            });
    }
}