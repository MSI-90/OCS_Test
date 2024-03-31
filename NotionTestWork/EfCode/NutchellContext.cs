using Microsoft.EntityFrameworkCore;
using NotionTestWork.Models.EfClasses;

namespace TestWorkForNotion.EfCode
{
    public class NutchellContext : DbContext
    {
        IConfiguration _configuration;
        public DbSet<User> users { get; set; }
        public DbSet<Application> applications { get; set; }

        public NutchellContext(DbContextOptions<NutchellContext> options, IConfiguration config) : base(options) =>
            _configuration = config;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(_configuration.GetConnectionString("defaultConnection"));

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasData(
        //        new User { Id = Guid.NewGuid(), UserName = "Professor", Email = "prof@gmail.com" },
        //        new User { Id = Guid.NewGuid(), UserName = "Student", Email = "student@gmail.com" }
        //    );
        //}
    }
}
