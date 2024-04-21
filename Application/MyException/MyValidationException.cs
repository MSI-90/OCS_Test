using System.Net;

namespace Application.MyException;
public class MyValidationException : Exception
{
    public List<string> ServerMessage { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public MyValidationException(string messages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        ServerMessage = new List<string>() { messages };
        StatusCode = statusCode;
    }
    public MyValidationException(List<string> list, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        ServerMessage = list;
        StatusCode = statusCode;
    }
}
