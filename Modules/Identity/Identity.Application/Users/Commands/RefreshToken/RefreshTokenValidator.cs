using BuildingBlocks.Application.Abstractions;
using BuildingBlocks.Application.Results;

namespace Identity.Application.Users.Commands.RefreshToken;

public sealed class RefreshTokenUserValidator
    : IValidator<RefreshTokenCommand>
{
    public List<Error> Validate(
        RefreshTokenCommand command)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(command.RefreshToken))
        {
            errors.Add(
                new Error(
                    "Users.RefreshToken.Empty",
                    "User RefreshToken is required",
                    ErrorType.Validation));
        }

        //TODO validators

        return errors;
    }
}