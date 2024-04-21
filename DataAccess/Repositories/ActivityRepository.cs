using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using TestWorkForNotion.DataAccess;

namespace DataAccess.Repositories;
public class ActivityRepository(ApplicationContext _context) : IActivityRepository
{
    public async Task<ICollection<ActivityType>> GetActivityTypesAsync()
    {
        return await _context.Activities.ToListAsync();
    }
}
