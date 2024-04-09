using System.Net;

namespace Application.Dto;

public class ExceptionRequest
{
    public List<string> ErrorMessage { get; set; }
    public ExceptionRequest() => ErrorMessage = new List<string>();
}
