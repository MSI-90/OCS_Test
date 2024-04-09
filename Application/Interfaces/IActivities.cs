using Application.Dto.Activity;

namespace Application.Interfaces;
public interface IActivities
{
    IEnumerable<ActivitiesResponse> GetActivities();
}
