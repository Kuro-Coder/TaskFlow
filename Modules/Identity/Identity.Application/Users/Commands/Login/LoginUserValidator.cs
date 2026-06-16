
namespace Identity.Application.Users.Commands.Login;

public sealed class LoginUserValidator
    : IValidator<LoginUserCommand>
{
    public List<Error> Validate(
        LoginUserCommand command)
    {
        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(command.Password))
        {
            errors.Add(
                new Error(
                    "Users.Password.Empty",
                    "User Password is required",
                    ErrorType.Validation));
        }

        if (string.IsNullOrWhiteSpace(command.Email))
        {
            errors.Add(
                new Error(
                    "Users.Email.Empty",
                    "User Email is required",
                    ErrorType.Validation));
        }

        //TODO validators

        return errors;
    }
}