
using BuildingBlocks.Application.Results;

namespace BuildingBlocks.Application.Messaging;

public interface ICommand<TResult>
{
}

public interface ICommand : ICommand<Result>
{
}