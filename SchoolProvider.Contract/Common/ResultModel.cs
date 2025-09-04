using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolProvider.Contract.Enums;

namespace SchoolProvider.Contract.Common;

public class ResultModel<T> : IActionResult
{
    public required bool IsSuccess { get; set; }
    public required HttpStatusCode? Code { get; set; }
    public required string ErrorMessage { get; set; }
    public required ResultType ResultType { get; set; }
    public required T Result { get; set; }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(this)
        {
            StatusCode = (int)Code!
        };
        
        await objectResult.ExecuteResultAsync(context);
    }
}