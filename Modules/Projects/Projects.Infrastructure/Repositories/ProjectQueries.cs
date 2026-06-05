using BuildingBlocks.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Projects.Application.Queries;
using Projects.Application.Queries.GetList;
using Projects.Infrastructure.Persistence;

namespace Projects.Infrastructure.Repositories;


public sealed class ProjectQueries : IProjectQueries
{
    private readonly ProjectsDbContext _context;

    public async Task<PagedResult<ProjectListItemResponse>> GetPagedAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var totalCount = await _context.Projects.CountAsync(cancellationToken);

        var items = await _context.Projects
            .OrderBy(x => x.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new ProjectListItemResponse(
                x.Id,
                x.Name))
            .ToListAsync(cancellationToken);

        return new PagedResult<ProjectListItemResponse>(
            items,
            page,
            pageSize,
            totalCount);
    }

}