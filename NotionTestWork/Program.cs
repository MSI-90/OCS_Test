using Api.Middlewares.ExceptionMiddleware;
using Api.Orchestrator;
using Application.Dto;
using Application.Dto.Applications;
using Application.Interfaces;
using Application.Services.Activities;
using Application.Services.Application;
using Microsoft.EntityFrameworkCore;
using NotionTestWork.DataAccess.Repositories;
using TestWorkForNotion.DataAccess;

namespace NotionTestWork;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<IApplicationDbContext, ApplicationContext>();

        builder.Services.AddSingleton<IActivities, ActivitiesService>();
        builder.Services.AddTransient<IApplicationRepository, ApplicationRepository>();
        builder.Services.AddTransient<IApplicationService, ApplicationService>();
        builder.Services.AddTransient<IRequestHandler<ApplicationsFromDateQuery, IEnumerable<ApplicationResponse>>, GetDateTimeAsQueryHandlercs>();

        builder.Services.AddExceptionHandler<ValidatonExceptionHandler>();
        builder.Services.AddExceptionHandler<InternalServerExceptionHandler>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
            applicationDbContext.Database.Migrate();
        }

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseExceptionHandler("/errors");

        app.UseRouting();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
