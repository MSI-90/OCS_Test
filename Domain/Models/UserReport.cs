namespace NotionTestWork.Domain.Models;

public class UserReport
{
    public Guid Id { get; set; }
    public Guid Author { get; set; }
    public ActivityEnum Activity { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Outline { get; set; } = string.Empty;
    public bool IsSubmitted { get; set; } = false;
}
