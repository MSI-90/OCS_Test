using Domain.Models;

namespace Application.Interfaces;
public interface IActivityRepository
{
    Task<ICollection<ActivityType>> GetActivityTypesAsync();
}
