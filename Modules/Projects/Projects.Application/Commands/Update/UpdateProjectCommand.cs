using BuildingBlocks.Application.Messaging;

namespace Projects.Application.Commands.Update;

public sealed record UpdateProjectCommand(
    Guid Id,
    string Name)
    : ICommand<bool>;