using BuildingBlocks.Application.Messaging;

namespace Projects.Application.Queries.GetList;

public sealed record GetProjectsQuery
    : IQuery<List<ProjectListItemResponse>>;