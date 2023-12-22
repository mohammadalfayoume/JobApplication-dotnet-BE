using JobApplication.API.Response;
using JobApplication.Service.Services;
using System;
using System.Net;
using System.Text.Json;

namespace JobApplication.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, 
        ILogger<ExceptionMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            context.Response.ContentType = "application/json";
            ApiException response;
            if (exception is ExceptionService serviceException)
            {

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = new ApiException(400, serviceException.Message);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response = new ApiException(500, exception.Message, exception.StackTrace.ToString());
            }
            

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }
}
