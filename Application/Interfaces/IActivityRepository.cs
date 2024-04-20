using Domain.Models;

namespace Application.Interfaces;
public interface IActivityRepository
{
    IEnumerable<ActivityType> GetActivityTypes();
}
