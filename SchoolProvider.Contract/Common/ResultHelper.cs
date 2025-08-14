using System.Net;
using SchoolProvider.Contract.Enums;

namespace SchoolProvider.Contract.Common;

public static class Result
{
    public static ResultModel<T> AsWarning<T>(this T value, string errorMessage = "", HttpStatusCode? code = HttpStatusCode.BadRequest)
    {
        var result = new ResultModel<T>
        {
            IsSuccess = false,
            Code = code,
            ErrorMessage = errorMessage,
            Result = value,
            ResultType = ResultType.Warning
        };

        return result;
    }
    
    public static ResultModel<T> AsError<T>(this T value, string errorMessage = "", HttpStatusCode? code = HttpStatusCode.BadRequest)
    {
        var result = new ResultModel<T>
        {
            IsSuccess = false,
            Code = code,
            ErrorMessage = errorMessage,
            Result = value,
            ResultType = ResultType.Error
        };

        return result;
    }

    public static ResultModel<T> AsSuccess<T>(this T value, string errorMessage = "")
    {
        var result = new ResultModel<T>
        {
            IsSuccess = true,
            Code = HttpStatusCode.OK,
            ErrorMessage = errorMessage,
            Result = value,
            ResultType = ResultType.Success
        };

        return result;
    }
}