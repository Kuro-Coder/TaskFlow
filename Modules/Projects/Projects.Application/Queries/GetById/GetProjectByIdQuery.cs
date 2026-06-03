using BuildingBlocks.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Application.Queries.GetById;

public sealed record GetProjectByIdQuery(
    Guid Id)
    : IQuery<ProjectResponse>;