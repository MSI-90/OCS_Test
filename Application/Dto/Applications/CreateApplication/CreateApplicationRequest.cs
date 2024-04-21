using NotionTestWork.Domain;

namespace Application.Dto.Applications.CreateApplication;

public class CreateApplicationRequest
{
    public Guid Author { get; set; }
    public ActivityEnum Activity { get; set; } = ActivityEnum.Report;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Outline { get; set; } = string.Empty;
}
