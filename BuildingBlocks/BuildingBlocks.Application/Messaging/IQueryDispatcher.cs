
using BuildingBlocks.Application.Results;

namespace BuildingBlocks.Application.Messaging;

public interface IQueryDispatcher
{
    Task<Result<TResult>> Dispatch<TResult>(
        IQuery<TResult> query,
        CancellationToken cancellationToken = default);
}