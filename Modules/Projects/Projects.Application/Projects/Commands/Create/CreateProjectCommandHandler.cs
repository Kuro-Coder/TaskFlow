using BuildingBlocks.Application.Abstractions;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Results;
using Projects.Domain.Entities.Projects;
using Projects.Domain.Repositories;

namespace Projects.Application.Projects.Commands.Create;

public sealed class CreateProjectCommandHandler
    : ICommandHandler<CreateProjectCommand, Guid>
{
    private readonly IProjectRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectCommandHandler(
        IProjectRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateProjectCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var project = Project.Create(
                command.Name);

            await _repository.AddAsync(
                project,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return Result<Guid>.Success(
                project.Id);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}