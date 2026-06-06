using BuildingBlocks.Application.Messaging;

namespace Projects.Application.Projects.Commands.Create;

public sealed record CreateProjectCommand(
    string Name)
    : ICommand<Guid>;