using BuildingBlocks.Application.Abstractions;
using BuildingBlocks.Application.Results;

namespace Identity.Application.Users.Commands.Register;

public sealed class RegisterUserValidator
    : IValidator<RegisterUserCommand>
{
    public List<Error> Validate(
        RegisterUserCommand command)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(command.FirstName))
        {
            errors.Add(
                new Error(
                    "Users.Name.Empty",
                    "User FirstName is required",
                    ErrorType.Validation));
        }

        //TODO validators

        return errors;
    }
}