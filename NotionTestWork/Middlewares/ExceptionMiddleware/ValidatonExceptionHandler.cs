using Microsoft.AspNetCore.Diagnostics;
using System.Net;
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

        var errorsFromExceptionAsString = string.Empty;
        if (validationException.ServerMessage.Count > 1)
        {
            foreach (var message in validationException.ServerMessage)
                errorsFromExceptionAsString += message + "/n";
        }
        else
            errorsFromExceptionAsString = validationException.ServerMessage[0];

        _logger.LogWarning(errorsFromExceptionAsString);
        httpContext.Response.StatusCode = (int)validationException.StatusCode;
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorsFromExceptionAsString));

        return ValueTask.FromResult(true);
    }
}
