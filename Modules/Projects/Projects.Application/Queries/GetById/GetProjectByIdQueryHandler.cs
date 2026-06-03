using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Results;
using Projects.Domain.Repositories;

namespace Projects.Application.Queries.GetById;

public sealed class GetProjectByIdQueryHandler
    : IQueryHandler<GetProjectByIdQuery, ProjectResponse>
{
    private readonly IProjectRepository _repository;

    public GetProjectByIdQueryHandler(
        IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProjectResponse>> Handle(
        GetProjectByIdQuery query,
        CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(
            query.Id,
            cancellationToken);

        if (project is null)
        {
            return Result<ProjectResponse>.Failure(
                new Error(
                    "Projects.NotFound",
                    "Project not found"));
        }

        return Result<ProjectResponse>.Success(
            new ProjectResponse(
                project.Id,
                project.Name));
    }
}