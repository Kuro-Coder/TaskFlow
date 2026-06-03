using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Results;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Infrastructure.Messaging;

public sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<Result<TResult>> Dispatch<TResult>(
        IQuery<TResult> query,
        CancellationToken cancellationToken = default)
    {
        var handlerType =
            typeof(IQueryHandler<,>)
                .MakeGenericType(
                    query.GetType(),
                    typeof(TResult));

        dynamic handler =
            _serviceProvider.GetRequiredService(handlerType);

        return await handler.Handle(
            (dynamic)query,
            cancellationToken);
    }
}