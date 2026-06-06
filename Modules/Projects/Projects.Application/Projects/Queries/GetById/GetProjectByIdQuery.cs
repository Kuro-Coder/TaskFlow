using BuildingBlocks.Application.Messaging;

namespace Projects.Application.Projects.Queries.GetById;

public sealed record GetProjectByIdQuery(
    Guid Id)
    : IQuery<GetProjectByIdResponse>;