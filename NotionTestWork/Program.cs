
using MyTaskManager.Middleware;
using NotionTestWork.Repositories;
using TestWorkForNotion.EfCode;

namespace NotionTestWork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen();

            //EF_Core service
            builder.Services.AddDbContext<NutchellContext>();

            //my_services
            builder.Services.AddScoped<IApplication, ApplicationRepo>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
