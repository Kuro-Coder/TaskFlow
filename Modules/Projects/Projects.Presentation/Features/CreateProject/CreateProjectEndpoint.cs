using BuildingBlocks.Application.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projects.Application.Commands.Create;

namespace Projects.Presentation.Features.CreateProject;

[ApiController]
[Route("api/projects")]
[Tags("Projects")]
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
        var result = await _dispatcher.Dispatch(
            new CreateProjectCommand(
                request.Name),
            cancellationToken);

        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(
            new CreateProjectResponse(
                result.Value));
    }
}