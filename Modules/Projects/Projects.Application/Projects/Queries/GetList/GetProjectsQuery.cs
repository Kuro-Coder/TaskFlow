using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Shared;

namespace Projects.Application.Projects.Queries.GetList;

public sealed record GetProjectsQuery(
    int Page,
    int PageSize)
    : IQuery<PagedResult<GetProjectsResponse>>;