using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace Api.Middlewares.ExceptionMiddleware;
public class InternalServerExceptionHandler : IExceptionHandler
{
    private readonly ILogger<InternalServerExceptionHandler> _logger;
    public InternalServerExceptionHandler(ILogger<InternalServerExceptionHandler> logger) => _logger = logger;

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception.Message);
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.WriteAsync(JsonSerializer.Serialize(exception.Message));
        return ValueTask.FromResult(true);
    }
}
