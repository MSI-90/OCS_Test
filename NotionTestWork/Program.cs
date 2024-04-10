using Application.Interfaces;
using Application.Services.Activities;
using Application.Services.Application;
using Microsoft.EntityFrameworkCore;
using NotionTestWork.Api.Middleware;
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

        builder.Services.AddDbContext<IApplicationDbContext, ApplicationContext>();

        builder.Services.AddTransient<IApplicationRepository, ApplicationRepository>();
        builder.Services.AddTransient<IApplicationService, ApplicationService>();
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

        //app.UseMiddleware<ErrorHandlingMiddleware>();

        //app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
