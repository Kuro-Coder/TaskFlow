
using BuildingBlocks.Application.Results;

namespace BuildingBlocks.Application.Messaging;

public interface ICommandDispatcher
{
    Task<Result<TResult>> Dispatch<TResult>(
        ICommand<TResult> command,
        CancellationToken cancellationToken = default);
}