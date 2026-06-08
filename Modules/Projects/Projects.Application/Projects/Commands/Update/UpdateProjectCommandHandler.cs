using BuildingBlocks.Application.Abstractions;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Results;
using Projects.Domain.Repositories;

namespace Projects.Application.Projects.Commands.Update;

public sealed class UpdateProjectCommandHandler
    : ICommandHandler<UpdateProjectCommand, bool>
{
    private readonly IProjectRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProjectCommandHandler(
        IProjectRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(
        UpdateProjectCommand command,
        CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdForUpdateAsync(
            command.Id,
            cancellationToken);

        if (project is null)
        {
            return Result<bool>.Failure(
                new Error(
                    "Projects.NotFound",
                    "Project not found", 
                    ErrorType.NotFound));
        }

        project.UpdateName(command.Name);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return Result<bool>.Success(true);
    }
}