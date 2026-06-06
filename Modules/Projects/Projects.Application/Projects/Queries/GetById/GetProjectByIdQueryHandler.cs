using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Results;
using Projects.Domain.Repositories;

namespace Projects.Application.Projects.Queries.GetById;

public sealed class GetProjectByIdQueryHandler
    : IQueryHandler<GetProjectByIdQuery, GetProjectByIdResponse>
{
    private readonly IProjectRepository _repository;

    public GetProjectByIdQueryHandler(
        IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetProjectByIdResponse>> Handle(
        GetProjectByIdQuery query,
        CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(
            query.Id,
            cancellationToken);

        if (project is null)
        {
            return Result<GetProjectByIdResponse>.Failure(
                new Error(
                    "Projects.NotFound",
                    "Project not found"));
        }

        return Result<GetProjectByIdResponse>.Success(
            new GetProjectByIdResponse(
                project.Id,
                project.Name));
    }
}