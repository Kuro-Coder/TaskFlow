using BuildingBlocks.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Projects.Application.Abstractions;
using Projects.Application.Projects.Queries.GetList;
using Projects.Infrastructure.Persistence;

namespace Projects.Infrastructure.Repositories;

public sealed class ProjectQueries : IProjectQueries
{
    private readonly ProjectsDbContext _context;
    public ProjectQueries(
        ProjectsDbContext context)
    {
        _context = context;
    }


    public async Task<PagedResult<GetProjectsResponse>> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var totalCount = await _context.Projects.CountAsync(cancellationToken);

        var items = await _context.Projects
            .OrderBy(x => x.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new GetProjectsResponse(
                x.Id,
                x.Name))
            .ToListAsync(cancellationToken);

        return new PagedResult<GetProjectsResponse>(
            items,
            page,
            pageSize,
            totalCount);
    }

}