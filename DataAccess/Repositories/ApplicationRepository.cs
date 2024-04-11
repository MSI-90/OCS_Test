﻿using Application.Dto.Applications;
using Application.Dto.Applications.CreateApplication;
using Application.Dto.Applications.UpdateApplication;
using Microsoft.EntityFrameworkCore;
using NotionTestWork.Domain.Models;
using TestWorkForNotion.DataAccess;
using static System.Net.Mime.MediaTypeNames;


namespace NotionTestWork.DataAccess.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationContext _context;
    public ApplicationRepository(ApplicationContext context) => _context = context;

    public async Task<bool> ApplicationExistForUserAsync(Guid userId)
    {
        return await _context.Applications.AnyAsync(a => a.IsSubmitted == false && a.Author == userId);
    }

    public async Task CreateApplication(UserReport createRequest)
    {
        _context.Applications.Add(createRequest);
        await _context.SaveChangesAsync();
    }

    public async Task<UserReport?> GetApplicationById(Guid id)
    {
        var application = await _context.Applications.SingleOrDefaultAsync(a => a.Id == id);
        return application;
    }

    public async Task<UserReport> UpdateApplicationAsync(UpdateApplicationRequest newData, UserReport oldData, Guid id)
    {
        oldData.Activity = newData.Activity;
        oldData.Name = string.IsNullOrEmpty(newData.Name) ? oldData.Name : newData.Name;
        oldData.Description = string.IsNullOrEmpty(newData.Description) ? oldData.Description : newData.Description;
        oldData.Outline = string.IsNullOrEmpty(newData.Outline) ? oldData.Outline : newData.Outline;
        oldData.CreatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return oldData;
    }

    public async Task DeleteApplicationById(UserReport application)
    {
        _context.Applications.Remove(application);
        await _context.SaveChangesAsync();
    }

    public async Task SendApplicationAsync(UserReport application)
    {
        application.IsSubmitted = true;
        await _context.SaveChangesAsync();
    }

    //получаем отправленные заявки поданые после указаной даты
    //public async Task<IEnumerable<ApplicationResponse>> GetApplicationIfSubmittedAsync(DateTime date)
    //{
    /*
    var dateTime = date.ToUniversalTime();
    var application = await _context.applications.Include(a => a.Author).Where(a => a.CreatedAt > dateTime && a.IsSubmitted).ToListAsync();

    var aapplicationResponses = application.Select(a => new ApplicationResponse
    {
        Id = a.Id,
        Author = a.Author.Id,
        Activity = a.Activity,
        Name = a.Name,
        Description = a.Description,
        Outline = a.Outline
    });

    return aapplicationResponses;
    
    //    return null;
    //}

    //получаем не поданые заявки и старше указаной даты
    //public async Task<IEnumerable<ApplicationResponse>> GetUnsobmitedApplicationAsync(DateTime date)
    //{
    /*
    var dateTime = date.ToUniversalTime();
    var application = await _context.applications.Include(a => a.Author).Where(a => a.CreatedAt > dateTime && a.IsSubmitted == false).ToListAsync();

    var aapplicationResponses = application.Select(a => new ApplicationResponse
    {
        Id = a.Id,
        Author = a.Author.Id,
        Activity = a.Activity,
        Name = a.Name,
        Description = a.Description,
        Outline = a.Outline
    });

    return aapplicationResponses;
    */
    //    return null;
    //}

    //public async Task<ApplicationResponse> GetCurrentApplication(Guid id)
    //{
    /*
    var user = await _context.users.FindAsync(id);
    if (user is null)
        throw new Exception($"Пользователь с данным идентификатором {id} не найден");

    var application = await _context.applications.Include(a => a.Author).FirstOrDefaultAsync(a => a.Author.Id == user.Id && a.IsSubmitted == false);
    if (application is null)
        throw new Exception($"Нет неотправленной заявки");

    return new ApplicationResponse
    {
        Id = application.Id,
        Author = application.Author.Id,
        Activity = application.Activity,
        Name = application.Name,
        Description = application.Description,
        Outline = application.Outline
    };
    */
    //    return null;
    //}
}
