
namespace BuildingBlocks.Application.Results;

public sealed record Error(
    string Code,
    string Message);