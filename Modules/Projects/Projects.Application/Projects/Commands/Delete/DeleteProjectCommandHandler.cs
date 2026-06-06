using BuildingBlocks.Application.Abstractions;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Results;
using Projects.Domain.Repositories;

namespace Projects.Application.Projects.Commands.Delete;

public sealed class DeleteProjectCommandHandler
    : ICommandHandler<DeleteProjectCommand, bool>
{
    private readonly IProjectRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProjectCommandHandler(
        IProjectRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        DeleteProjectCommand command,
        CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdForDeleteAsync(
            command.Id,
            cancellationToken);

        if (project is null)
        {
            return Result<bool>.Failure(
                new Error(
                    "Projects.NotFound",
                    "Project not found"));
        }

        project.Delete();

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Result<bool>.Success(true);
    }
}