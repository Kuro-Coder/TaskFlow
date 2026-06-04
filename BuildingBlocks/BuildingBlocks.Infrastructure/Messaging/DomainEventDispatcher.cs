using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure.Messaging;

public sealed class DomainEventDispatcher
    : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Dispatch(
        IDomainEvent domainEvent,
        CancellationToken cancellationToken)
    {
        var handlerType =
            typeof(IDomainEventHandler<>)
                .MakeGenericType(
                    domainEvent.GetType());

        var handlers =
            _serviceProvider
                .GetServices(handlerType);

        foreach (dynamic handler in handlers)
        {
            await handler.Handle(
                (dynamic)domainEvent,
                cancellationToken);
        }
    }
}