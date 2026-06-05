using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Results;
using BuildingBlocks.Domain.Shared;

namespace Projects.Application.Queries.GetList;

public sealed class GetProjectsQueryHandler
    : IQueryHandler<
        GetProjectsQuery,
        PagedResult<ProjectListItemResponse>>
{
    private readonly IProjectQueries _queries;

    public GetProjectsQueryHandler(
        IProjectQueries repository)
    {
        _queries = repository;
    }

    public async Task<Result<PagedResult<ProjectListItemResponse>>> Handle(
        GetProjectsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _queries.GetPagedAsync(
            query.Page,
            query.PageSize,
            cancellationToken);

        return Result<PagedResult<ProjectListItemResponse>>
            .Success(result);
    }
}