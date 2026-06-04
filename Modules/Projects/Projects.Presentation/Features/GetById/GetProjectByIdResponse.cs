namespace Projects.Presentation.Features.GetById;


public sealed record GetProjectByIdResponse(
    Guid Id,
    string Name);