using BuildingBlocks.Application.Abstractions;
using BuildingBlocks.Application.Messaging;
using BuildingBlocks.Application.Results;
using Projects.Domain.Entities;
using Projects.Domain.Repositories;

namespace Projects.Application.Projects.Commands.Create;

public sealed class CreateProjectValidator
    : IValidator<CreateProjectCommand>
{
    public List<Error> Validate(
        CreateProjectCommand command)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(command.Name))
        {
            errors.Add(
                new Error(
                    "Projects.Name.Empty",
                    "Project name is required"));
        }

        if (command.Name.Length > 100)
        {
            errors.Add(
                new Error(
                    "Projects.Name.TooLong",
                    "Project name is too long"));
        }

        return errors;
    }
}