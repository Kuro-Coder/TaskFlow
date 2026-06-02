
namespace BuildingBlocks.Application.Messaging;

public interface ICommandHandler<TCommand, TResult>
{
    //Task<TResult> Handle(
    //    TCommand command,
    //    CancellationToken cancellationToken);
}