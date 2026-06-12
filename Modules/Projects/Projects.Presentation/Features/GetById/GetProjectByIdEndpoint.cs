using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projects.Application.Projects.Queries.GetById;

namespace Projects.Presentation.Features.GetById;

[ApiController]
[Route("api/projects")]
[Tags("Projects")]
[Authorize]
public sealed class GetProjectByIdEndpoint
    : ControllerBase
{
    private readonly IQueryDispatcher _dispatcher;

    public GetProjectByIdEndpoint(
        IQueryDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet("{id:guid}")]
    public async Task<IResult> Handle(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Dispatch(
            new GetProjectByIdQuery(id),
            cancellationToken);
        if (result.IsFailure)
            return result.ToProblemResult();

        return Results.Ok(result.Value!);
    }
}