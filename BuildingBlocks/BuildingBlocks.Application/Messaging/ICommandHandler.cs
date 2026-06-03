
using BuildingBlocks.Application.Results;

namespace BuildingBlocks.Application.Messaging;

public interface ICommandHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
    Task<Result<TResult>> Handle(
        TCommand command,
        CancellationToken cancellationToken);
}
public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task<Result> Handle(
        TCommand command,
        CancellationToken cancellationToken);
}