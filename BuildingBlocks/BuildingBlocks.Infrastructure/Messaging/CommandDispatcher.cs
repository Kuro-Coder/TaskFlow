using BuildingBlocks.Application.Abstractions;
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

        var validatorType =
            typeof(IValidator<>)
                .MakeGenericType(command.GetType());

        dynamic? validator =
            _serviceProvider.GetService(validatorType);
        if (validator is not null)
        {
            var errors = validator.Validate((dynamic)command);

            if (errors.Count > 0)
            {
                return Result<TResult>.Failure(
                    errors[0]);
            }
        }

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
}