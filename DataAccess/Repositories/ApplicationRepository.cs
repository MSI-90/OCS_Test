﻿using Application.Dto.Applications.UpdateApplication;
using Microsoft.EntityFrameworkCore;
using NotionTestWork.Domain.Models;
using TestWorkForNotion.DataAccess;


namespace NotionTestWork.DataAccess.Repositories;

public class ApplicationRepository(ApplicationContext _context) : IApplicationRepository
{
    public async Task<bool> ApplicationExistForUserAsync(Guid userId) => await _context.Applications.AnyAsync(a => a.IsSubmitted == false && a.Author == userId);

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

    public async Task<IEnumerable<UserReport>> GetApplicationIfSubmittedAsync(DateTime date)
    {
        var dateTime = date.ToUniversalTime();
        var application = await _context.Applications.Where(a => a.CreatedAt > dateTime && a.IsSubmitted).ToListAsync();
        return application;
    }

    public async Task<IEnumerable<UserReport>> GetUnsobmitedApplicationAsync(DateTime date)
    {
        var dateTime = date.ToUniversalTime();
        var application = await _context.Applications.Where(a => a.CreatedAt < dateTime && a.IsSubmitted == false).ToListAsync();
        return application;
    }

    public async Task<UserReport?> GetCurrentApplication(Guid userId) =>
        await _context.Applications.FirstOrDefaultAsync(a => a.Author == userId && a.IsSubmitted == false);

    public async Task<bool> UserExist(Guid userId) => await _context.Applications.AnyAsync(a => a.Author == userId);
}
