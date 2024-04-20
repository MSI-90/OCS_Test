using DataAccess.Configuration;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotionTestWork.Domain.Models;

namespace TestWorkForNotion.DataAccess;

public class ApplicationContext : DbContext
{
    private readonly IConfiguration _configuration;

    public DbSet<UserReport> Applications { get; set; }
    public DbSet<ActivityType> Activities { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration config) : base(options) =>
        _configuration = config;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("defaultConnection"));
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
        modelBuilder.ApplyConfiguration(new ActivityConfiguration());
    }
}
