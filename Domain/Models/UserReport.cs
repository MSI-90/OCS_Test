
namespace NotionTestWork.Domain.Models;

public class UserReport
{
    public Guid Id { get; set; }
    public Guid Author { get; set; }
    public ActivityEnum Activity { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Outline { get; set; }
    public bool IsSubmitted { get; set; } = false;
}
