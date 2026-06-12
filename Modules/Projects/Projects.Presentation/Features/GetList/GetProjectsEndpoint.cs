using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Presentation.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projects.Application.Projects.Queries.GetList;

namespace Projects.Presentation.Features.GetList;

[ApiController]
[Route("api/projects")]
[Tags("Projects")]
[Authorize]
public sealed class GetProjectsEndpoint
    : ControllerBase
{
    private readonly IQueryDispatcher _dispatcher;

    public GetProjectsEndpoint(
        IQueryDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet]
    public async Task<IResult> Handle(
        [FromQuery] GetProjectsRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Dispatch(new GetProjectsQuery(
            request.Page,
            request.PageSize), cancellationToken);
        if (result.IsFailure)
            return result.ToProblemResult();

        return Results.Ok(result.Value);
    }
}