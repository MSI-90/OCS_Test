using Application.Services;
using Microsoft.EntityFrameworkCore;
using NotionTestWork.Api.Middleware;
using NotionTestWork.Application.Services;
using NotionTestWork.DataAccess.Repositories;
using TestWorkForNotion.DataAccess;

namespace NotionTestWork;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen();

        //EF_Core service
        builder.Services.AddDbContext<ApplicationContext>();

        //my_services
        builder.Services.AddScoped<IApplication, ApplicationRepo>();
        builder.Services.AddSingleton<IActivities, ActivitiesService>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            applicationDbContext.Database.Migrate();
        }

        // Configure the HTTP request pipeline.
        //if (app.Environment.IsDevelopment())
        //{
        app.UseSwagger();
        app.UseSwaggerUI();
        //}

        app.UseMiddleware<ErrorHandlingMiddleware>();

        //app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
