using Application.Interfaces;
using DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotionTestWork.Domain.Models;

namespace TestWorkForNotion.DataAccess;

public class ApplicationContext : DbContext, IApplicationDbContext
{
    private readonly IConfiguration _configuration;
    public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration config) : base(options) =>
        _configuration = config;
    public DbSet<UserReport> Applications { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("defaultConnection"));
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
    }
}
