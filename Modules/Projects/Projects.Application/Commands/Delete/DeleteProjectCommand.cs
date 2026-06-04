using BuildingBlocks.Application.Messaging;

namespace Projects.Application.Commands.Delete;

public sealed record DeleteProjectCommand(
    Guid Id)
    : ICommand<bool>;