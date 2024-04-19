using NotionTestWork.Domain;

namespace Domain.Models;
public class ActivityType
{
    public int Id { get; set; }
    public ActivityEnum TypeOfActivity { get; set; }
    public string Description { get; set; } = string.Empty;
}
