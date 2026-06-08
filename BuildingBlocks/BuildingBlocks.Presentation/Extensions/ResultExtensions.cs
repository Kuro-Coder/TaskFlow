using BuildingBlocks.Application.Results;
using Microsoft.AspNetCore.Http;

namespace BuildingBlocks.Presentation.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemResult(
        this Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException();

        return Results.Problem(
            title: result.Error!.Code,
            detail: result.Error.Message,
            statusCode: GetStatusCode(result.Error.Type));
    }


    private static int GetStatusCode(
    ErrorType type)
    {
        return type switch
        {
            ErrorType.Validation =>
                StatusCodes.Status400BadRequest,

            ErrorType.NotFound =>
                StatusCodes.Status404NotFound,

            ErrorType.Conflict =>
                StatusCodes.Status409Conflict,

            ErrorType.Unauthorized =>
                StatusCodes.Status401Unauthorized,

            _ =>
                StatusCodes.Status500InternalServerError
        };
    }
}