using System.Net;
using System.Text;
using System.Text.Json;
using Domain.Entities.Response;
using Domain.Exceptions;

namespace Controller.Middleware;

public class HandlerExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HandlerExceptionMiddleware> _logger;

    public HandlerExceptionMiddleware(RequestDelegate next, ILogger<HandlerExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var response = context.Response;

        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogInformation("Exception caught");

            response.ContentType = "application/json; charset=utf-8";
            response.StatusCode = e switch
            {
                FestpayForbiddenException => (int)HttpStatusCode.Forbidden,
                FestpayUnauthorizedException => (int)HttpStatusCode.Unauthorized,
                FestpayNotFoundException => (int)HttpStatusCode.NotFound,
                FestpayBadRequestException => (int)HttpStatusCode.BadRequest,
                FestpayException => (int)HttpStatusCode.BadGateway,
                HttpRequestException => (int)HttpStatusCode.BadGateway,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var json = JsonSerializer.Serialize(new ResponseApi(e));

            if (response.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                _logger.LogError(e.InnerException?.Message);
                _logger.LogError(e.Message);
            }

            var b = Encoding.ASCII.GetBytes(json);
            await response.Body.WriteAsync(b).AsTask();
            await response.StartAsync();
        }
    }
}