using BuildingBlocks.Domain.Shared;
using Projects.Application.Queries.GetList;

namespace Projects.Application.Queries;

public interface IProjectQueries
{
    Task<PagedResult<ProjectListItemResponse>> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken);
}
