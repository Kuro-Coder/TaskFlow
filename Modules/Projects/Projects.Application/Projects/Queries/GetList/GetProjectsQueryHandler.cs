using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Results;
using BuildingBlocks.Domain.Shared;
using Projects.Application.Abstractions;

namespace Projects.Application.Projects.Queries.GetList;

public sealed class GetProjectsQueryHandler : 
    IQueryHandler<GetProjectsQuery, PagedResult<GetProjectsResponse>>
{
    private readonly IProjectQueries _queries;

    public GetProjectsQueryHandler(
        IProjectQueries repository)
    {
        _queries = repository;
    }

    public async Task<Result<PagedResult<GetProjectsResponse>>> Handle(
        GetProjectsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _queries.GetPagedAsync(
            query.Page,
            query.PageSize,
            cancellationToken);

        return Result<PagedResult<GetProjectsResponse>>
            .Success(result);
    }
}