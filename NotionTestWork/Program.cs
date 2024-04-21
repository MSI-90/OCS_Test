using Api.Middlewares.ExceptionMiddleware;
using Api.Orchestrator;
using Application.Dto;
using Application.Dto.Applications;
using Application.Dto.Applications.CreateApplication;
using Application.Interfaces;
using Application.Services.Activities;
using Application.Services.Application;
using Application.Validations;
using DataAccess.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotionTestWork.DataAccess.Repositories;
using TestWorkForNotion.DataAccess;

namespace NotionTestWork;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApplicationContext>();

        builder.Services.AddScoped<IApplicationService, ApplicationService>();
        builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
        builder.Services.AddScoped<IActivityService, ActivitiesService>();
        builder.Services.AddScoped<IActivityRepository, ActivityRepository>();

        builder.Services.AddScoped<IRequestHandler<ApplicationsFromDateQuery, IEnumerable<ApplicationResponse>>, GetDateTimeAsQueryHandlercs>();

        builder.Services.AddScoped<IValidator<CreateApplicationRequest>, ApplicationRequestValidator>();

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
