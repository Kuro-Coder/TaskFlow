using BuildingBlocks.Application.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projects.Application.Queries.GetList;

namespace Projects.Presentation.Features.GetList;

[ApiController]
[Route("api/projects")]
[Tags("Projects")]
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
        CancellationToken cancellationToken)
    {
        var result = await _dispatcher.Dispatch(
            new GetProjectsQuery(),
            cancellationToken);

        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error);
        }

        return Results.Ok(
            result.Value!.Select(x =>
                new GetProjectsResponse(
                    x.Id,
                    x.Name)));
    }
}