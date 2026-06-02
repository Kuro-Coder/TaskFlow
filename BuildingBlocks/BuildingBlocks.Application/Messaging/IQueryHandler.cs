using BuildingBlocks.Application.Results;

namespace BuildingBlocks.Application.Messaging;

public interface IQueryHandler<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    Task<Result<TResult>> Handle(
        TQuery query,
        CancellationToken cancellationToken);
}