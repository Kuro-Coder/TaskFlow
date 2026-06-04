
namespace BuildingBlocks.Application.Results;

public sealed class ValidationResult
{
    public List<Error> Errors { get; }

    public bool IsValid => Errors.Count == 0;
}