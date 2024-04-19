using Application.Dto.Activity;

namespace Application.Interfaces;
public interface IActivityService
{
    IEnumerable<ActivitiesResponse> GetActivities();
}
