using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Results;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.Messaging;

public sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<Result<TResult>> Dispatch<TResult>(
        ICommand<TResult> command,
        CancellationToken cancellationToken = default)
    {
        var handlerType =
            typeof(ICommandHandler<,>)
                .MakeGenericType(
                    command.GetType(),
                    typeof(TResult));

        dynamic handler =
            _serviceProvider.GetRequiredService(handlerType);

        return await handler.Handle(
            (dynamic)command,
            cancellationToken);
    }

    public Task<Result> Dispatch(
        ICommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}