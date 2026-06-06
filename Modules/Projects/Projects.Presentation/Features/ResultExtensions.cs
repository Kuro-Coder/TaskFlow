using BuildingBlocks.Application.Results;
using Microsoft.AspNetCore.Http;

namespace Projects.Presentation.Features;

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
            statusCode: StatusCodes.Status400BadRequest);
    }
}