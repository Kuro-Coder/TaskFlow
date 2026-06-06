using BuildingBlocks.Application.Messaging;

namespace Projects.Application.Projects.Commands.Update;

public sealed record UpdateProjectCommand(
    Guid Id,
    string Name)
    : ICommand<bool>;