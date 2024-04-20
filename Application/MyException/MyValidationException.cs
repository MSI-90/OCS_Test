﻿using System.Net;

namespace Application.MyException;
public class MyValidationException : Exception
{
    public List<string> ServerMessage { get; set; } = new List<string>();
    public HttpStatusCode StatusCode { get; set; }
    public MyValidationException(string messages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        ServerMessage.Add(messages);
        StatusCode = statusCode;
    }
}
