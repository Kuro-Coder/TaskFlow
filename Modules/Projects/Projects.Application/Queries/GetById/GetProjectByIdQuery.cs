using BuildingBlocks.Application.Messaging;

namespace Projects.Application.Queries.GetById;

public sealed record GetProjectByIdQuery(
    Guid Id)
    : IQuery<ProjectResponse>;