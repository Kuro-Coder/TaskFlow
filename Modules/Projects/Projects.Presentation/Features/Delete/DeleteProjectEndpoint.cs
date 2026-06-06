using BuildingBlocks.Application.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projects.Application.Projects.Commands.Delete;

namespace Projects.Presentation.Features.Delete;

[ApiController]
[Route("api/projects")]
[Tags("Projects")]
public sealed class DeleteProjectEndpoint
    : ControllerBase
{
    private readonly ICommandDispatcher _dispatcher;

    public DeleteProjectEndpoint(
        ICommandDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpDelete("{id:guid}")]
    public async Task<IResult> Handle(
        Guid id,
        DeleteProjectRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Dispatch(new DeleteProjectCommand(
            id), cancellationToken);
        if (result.IsFailure)
            return result.ToProblemResult();

        return Results.NoContent();
    }
}