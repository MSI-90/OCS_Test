using NotionTestWork.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;
public interface IApplicationDbContext
{
    DbSet<UserReport> Applications { get; set; }
    Task<int> SaveChangesAsync();
    //Task<int> SaveChangesAsync(CancellationToken token);
}