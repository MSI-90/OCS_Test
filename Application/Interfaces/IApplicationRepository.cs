﻿using Application.Dto.Applications.UpdateApplication;
using NotionTestWork.Domain.Models;

namespace NotionTestWork.DataAccess.Repositories;
public interface IApplicationRepository
{
    Task<bool> ApplicationExistForUserAsync(Guid userId);
    Task CreateApplication(UserReport createApplication);
    Task<UserReport?> GetApplicationById(Guid id);
    Task<UserReport> UpdateApplicationAsync(UpdateApplicationRequest newData, UserReport oldData, Guid id);
    Task DeleteApplicationById(UserReport application);
    Task SendApplicationAsync(UserReport application);
    Task<IEnumerable<UserReport>> GetApplicationIfSubmittedAsync(DateTime date);
    Task<IEnumerable<UserReport>> GetUnsobmitedApplicationAsync(DateTime date);
    Task<UserReport?> GetCurrentApplication(Guid userId);
    Task<bool> UserExist(Guid userId);
}
