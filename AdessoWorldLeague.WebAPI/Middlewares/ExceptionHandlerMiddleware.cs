using System.Net;
using System.Text.Json;
using FluentResults;

namespace AdessoWorldLeague.WebAPI.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = Result.Fail("An error occurred while processing your request.");
            var resultAsJson = JsonSerializer.Serialize(result);
            await response.WriteAsync(resultAsJson);
        }
    }
}