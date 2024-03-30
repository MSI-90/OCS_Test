namespace NotionTestWork.Models
{
    public class ExceptionRequest
    {
        public List<string> ErrorMessage { get; set; }
        public ExceptionRequest() => ErrorMessage = new List<string>();
    }
}
