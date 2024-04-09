using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NotionTestWork.Domain.Models;

namespace TestWorkForNotion.DataAccess;

public class ApplicationContext : DbContext
{
    private readonly IConfiguration _configuration;
    public DbSet<Applications> Applications { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration config) : base(options) =>
        _configuration = config;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("defaultConnection"));
}
