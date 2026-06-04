
using BuildingBlocks.Application.Results;

namespace BuildingBlocks.Application.Abstractions;

public interface IValidator<in T>
{
    List<Error> Validate(T instance);
}