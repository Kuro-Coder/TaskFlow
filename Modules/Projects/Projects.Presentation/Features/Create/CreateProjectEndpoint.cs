using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projects.Application.Projects.Commands.Create;

namespace Projects.Presentation.Features.Create;

[ApiController]
[Route("api/projects")]
[Tags("Projects")]
[Authorize]
public sealed class CreateProjectEndpoint : ControllerBase
{
    private readonly ICommandDispatcher _dispatcher;

    public CreateProjectEndpoint(
        ICommandDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPost]
    public async Task<IResult> Handle(
        CreateProjectRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Dispatch(new CreateProjectCommand(
            request.Name), cancellationToken);
        if (result.IsFailure)
            return result.ToProblemResult();

        return Results.Ok(result.Value);
    }
}