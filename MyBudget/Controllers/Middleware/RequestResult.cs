using Domain.Entities.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Controller.Middleware;

public class RequestResult : IActionResult
{
    private readonly HttpStatusCode? _statusCode = null;
    private readonly object? _result;

    public RequestResult() { }

    public RequestResult(object? data, string? message = null)
    {
        if (data?.GetType() == typeof(ResultApi))
        {
            var r = (ResultApi)data;
            if (!r.Success)
            {
                _statusCode = HttpStatusCode.BadRequest;
            }

            _result = new ResponseApi(r.Success, null, r.Data);
        }
        else
        {
            _result = new ResponseApi(true, message, data);
        }
    }

    public async Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(_result)
        {
            StatusCode = (int)(_statusCode ?? (context.HttpContext.Request.Method == "POST" ? HttpStatusCode.Created : HttpStatusCode.OK))
        };

        await objectResult.ExecuteResultAsync(context);
    }
}