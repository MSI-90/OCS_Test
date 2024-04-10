namespace Api.Middlewares.ExceptionMiddleware;

public class MyValidationException : Exception
{
    public string ServerMessage { get; set; }
    public MyValidationException(string serverMessage) => ServerMessage = serverMessage;
}
