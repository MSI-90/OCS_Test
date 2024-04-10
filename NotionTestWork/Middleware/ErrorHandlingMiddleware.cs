using Application.Dto;
using System.Text.Json;

namespace NotionTestWork.Api.Middleware;

internal class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    //private readonly ILogger _logger;
    public ErrorHandlingMiddleware(RequestDelegate next /*ILogger logger*/)
    {
        this._next = next;
        //_logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        //_logger.LogError(exception.Message);
        var response = new ExceptionRequest();
        response.ErrorMessage.Add(exception.Message); //"Упс, сто-то пошло ни так, попробуйте повторить операцию позднее!"
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
