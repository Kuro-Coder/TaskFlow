using BuildingBlocks.Application.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projects.Application.Projects.Commands.Update;

namespace Projects.Presentation.Features.Update;

[ApiController]
[Route("api/projects")]
[Tags("Projects")]
public sealed class UpdateProjectEndpoint
    : ControllerBase
{
    private readonly ICommandDispatcher _dispatcher;

    public UpdateProjectEndpoint(
        ICommandDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpPut("{id:guid}")]
    public async Task<IResult> Handle(
        Guid id,
        UpdateProjectRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Dispatch(new UpdateProjectCommand(
            id,
            request.Name),
            cancellationToken);
        if (result.IsFailure)
            return Results.BadRequest(result.Error);

        return Results.NoContent();
    }
}