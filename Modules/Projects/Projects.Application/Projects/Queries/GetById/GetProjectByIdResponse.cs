namespace Projects.Application.Projects.Queries.GetById;

public sealed record GetProjectByIdResponse(
    Guid Id,
    string Name);