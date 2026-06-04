

namespace Projects.Application.Queries.GetById;

public sealed record ProjectResponse(
    Guid Id,
    string Name);