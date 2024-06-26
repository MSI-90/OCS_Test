﻿using Application.Dto.Applications.CreateApplication;
using Application.Dto.Applications.UpdateApplication;
using Application.Dto.Applications;
using NotionTestWork.Domain.Models;
using FluentResults;

namespace Application.Interfaces;
public interface IApplicationService
{
    Task<ApplicationResponse> CreateApplicationAsync(CreateApplicationRequest app);
    Task<ApplicationResponse> GetApplicationById(Guid id);
    Task<ApplicationResponse> UpdateApplicationAsync(UpdateApplicationRequest newData, Guid id);
    Task DeleteApplicationById(Guid id);
    Task SendApplicationAsync(Guid id);
    Task<IEnumerable<ApplicationResponse>> GetApplicationIfSubmittedAsync(DateTime? date);
    Task<IEnumerable<ApplicationResponse>> GetUnsobmitedApplicationAsync(DateTime? date);
    Task<ApplicationResponse> GetCurrentUnsubmittedApplicationForUserAsync(Guid id);
    bool VerificationPropertyAsNullOrEmpty(CreateApplicationRequest application);
    bool VerificationPropertyAsNullOrEmpty(UserReport application);
}
