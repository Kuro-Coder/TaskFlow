using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Results;
using Projects.Domain.Repositories;

namespace Projects.Application.Queries.GetList;

public sealed class GetProjectsQueryHandler
    : IQueryHandler<
        GetProjectsQuery,
        List<ProjectListItemResponse>>
{
    private readonly IProjectRepository _repository;

    public GetProjectsQueryHandler(
        IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<ProjectListItemResponse>>> Handle(
        GetProjectsQuery query,
        CancellationToken cancellationToken)
    {
        var projects = await _repository.GetAll(
            cancellationToken);

        var result = projects
            .Select(x => new ProjectListItemResponse(
                x.Id,
                x.Name))
            .ToList();

        return Result<List<ProjectListItemResponse>>
            .Success(result);
    }
}