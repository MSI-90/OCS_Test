using Application.Dto.Activity;

namespace Application.Interfaces;
public interface IActivityService
{
    Task<IEnumerable<ActivitiesResponse>> GetActivitiesAsync();
}
