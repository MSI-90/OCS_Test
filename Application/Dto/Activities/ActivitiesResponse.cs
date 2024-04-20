using NotionTestWork.Domain;

namespace Application.Dto.Activity;

public class ActivitiesResponse
{
    public ActivityEnum Activity { get; set; }
    public string Description { get; set; } = string.Empty;
}
