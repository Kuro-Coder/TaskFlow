using BuildingBlocks.Application.Messaging;

namespace Projects.Application.Commands.Create;

public sealed record CreateProjectCommand(
    string Name)
    : ICommand<Guid>;