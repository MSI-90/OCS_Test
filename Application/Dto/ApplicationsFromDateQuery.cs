namespace Application.Dto;
public class ApplicationsFromDateQuery
{
    public DateTime? SubmittedAfter { get; set; }
    public DateTime? UnsubmittedOlder { get; set; }
}
