using BuildingBlocks.Application.Messaging;

namespace Projects.Application.Projects.Commands.Delete;

public sealed record DeleteProjectCommand(
    Guid Id)
    : ICommand<bool>;