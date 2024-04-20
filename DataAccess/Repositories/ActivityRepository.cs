using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWorkForNotion.DataAccess;

namespace DataAccess.Repositories;
public class ActivityRepository(ApplicationContext _context) : IActivityRepository
{
    public IEnumerable<ActivityType> GetActivityTypes()
    {
        return _context.Activities.ToList();
    }
}
