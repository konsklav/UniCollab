using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Saas.Api.Extensions;

internal static class ResultExtensions
{
    public static IResult ToHttp<T, TSuccess>(
        this Result<T> result,
        Func<T, TSuccess>? onSuccess)
    {
        if (!result.IsSuccess || onSuccess is null)
            return result.ToMinimalApiResult();

        return Results.Ok(onSuccess(result.Value));
    }

    public static async Task<IResult> ToHttp<T, TSuccess>(
        this Task<Result<T>> resultTask,
        Func<T, TSuccess>? onSuccess = null)
    {
        var result = await resultTask;
        if (!result.IsSuccess || onSuccess is null)
            return result.ToMinimalApiResult();

        return Results.Ok(onSuccess(result.Value));
    }

    public static async Task<IResult> ToHttp(this Task<Result> resultTask)
    {
        var result = await resultTask;
        return result.ToMinimalApiResult();
    }
}