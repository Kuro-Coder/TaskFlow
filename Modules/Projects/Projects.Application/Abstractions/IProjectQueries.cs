using BuildingBlocks.Domain.Shared;
using Projects.Application.Projects.Queries.GetList;

namespace Projects.Application.Abstractions;

public interface IProjectQueries
{
    Task<PagedResult<GetProjectsResponse>> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken);
}
