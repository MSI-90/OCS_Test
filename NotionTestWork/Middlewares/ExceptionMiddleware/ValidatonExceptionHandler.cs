using Application.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Api.Middlewares.ExceptionMiddleware;

public class ValidatonExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ValidatonExceptionHandler> _logger;
    public ValidatonExceptionHandler(ILogger<ValidatonExceptionHandler> logger) => _logger = logger;
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not MyValidationException validationException)
            return ValueTask.FromResult(false);

        _logger.LogError(validationException.ServerMessage);
        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest; ;
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.WriteAsync(JsonSerializer.Serialize(validationException.ServerMessage));

        return ValueTask.FromResult(true);
    }
}
