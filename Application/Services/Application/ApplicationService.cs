using Application.Dto.Applications;
using Application.Dto.Applications.CreateApplication;
using Application.Dto.Applications.UpdateApplication;
using Application.Interfaces;
using Application.MyException;
using Microsoft.EntityFrameworkCore;
using NotionTestWork.DataAccess.Repositories;
using NotionTestWork.Domain.Models;
using System;
using System.Net;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services.Application;
public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _repository;
    public ApplicationService(IApplicationRepository repository)
    {
        _repository = repository;
    }
    public async Task<ApplicationResponse> CreateApplicationAsync(CreateApplicationRequest app)
    {
        var stringIsOk = VerificationPropertyAsNullOrEmpty(app);
        if (!stringIsOk)
            throw new MyValidationException("Ошибка при создании заявки, проверьте достоверность заполнения полей перед созданием заявки.");

        bool applicationAsUnsubmitForUserExist = await _repository.ApplicationExistForUserAsync(app.Author);
        if (applicationAsUnsubmitForUserExist)
            throw new MyValidationException("У Вас уже имеется заявка в статусе - не отправлена");

        var newApplicationToDb = new UserReport
        {
            Id = Guid.NewGuid(),
            Author = app.Author,
            Activity = app.Activity,
            CreatedAt = DateTime.UtcNow,
            Name = app.Name,
            Description = app.Description,
            Outline = app.Outline,
            IsSubmitted = false
        };

        await _repository.CreateApplication(newApplicationToDb);

        var newApplicationResponse = new ApplicationResponse
        {
            Id = newApplicationToDb.Id,
            Author = newApplicationToDb.Author,
            Activity = newApplicationToDb.Activity,
            Name = newApplicationToDb.Name,
            Description = newApplicationToDb.Description,
            Outline = newApplicationToDb.Outline
        };
        return newApplicationResponse;
    }

    public async Task DeleteApplicationById(Guid id)
    {
        var applicationExist = await _repository.GetApplicationById(id);
        if (applicationExist is null)
            throw new MyValidationException($"Нет заявки под id = {id}", HttpStatusCode.NotFound);

        if (applicationExist.IsSubmitted == true)
            throw new MyValidationException("Заявка была отправлена на проверку и не может быть удалена");

        await _repository.DeleteApplicationById(applicationExist);
    }

    public async Task<ApplicationResponse> GetApplicationById(Guid id)
    {
        var applicatoinFromRepository = await _repository.GetApplicationById(id);
        if (applicatoinFromRepository is null)
            throw new MyValidationException($"Нет заявки под id = {id}", HttpStatusCode.NotFound);

        return new ApplicationResponse
        {
            Id = applicatoinFromRepository.Id,
            Author = applicatoinFromRepository.Author,
            Activity = applicatoinFromRepository.Activity,
            Name = applicatoinFromRepository.Name,
            Description = applicatoinFromRepository.Description,
            Outline = applicatoinFromRepository.Outline
        };
    }

    public async Task<ApplicationResponse> UpdateApplicationAsync(UpdateApplicationRequest newData, Guid id)
    {
        var applicationFrorUpdate = await _repository.GetApplicationById(id);
        if (applicationFrorUpdate is null)
            throw new MyValidationException($"Нет заявки под id = {id}", HttpStatusCode.NotFound);

        if (applicationFrorUpdate.IsSubmitted == true)
            throw new MyValidationException("Данная заявка не может быть отредактирована, потому, что она была уже отправлена на проверку");

        var updatedApplication = await _repository.UpdateApplicationAsync(newData, applicationFrorUpdate, id);
        return new ApplicationResponse
        {
            Id = updatedApplication.Id,
            Author = updatedApplication.Author,
            Activity = updatedApplication.Activity,
            Name = updatedApplication.Name,
            Description = updatedApplication.Description,
            Outline = updatedApplication.Outline
        };
    }

    public async Task SendApplicationAsync(Guid id)
    {
        var application = await _repository.GetApplicationById(id);
        if (application is null)
            throw new MyValidationException($"Нет заявки под id = {id}", HttpStatusCode.NotFound);

        if (application.IsSubmitted == true)
            throw new MyValidationException("Данная заявка уже была отправлена на проверку ранее");

        var stringIsOk = VerificationPropertyAsNullOrEmpty(application);
        if (!stringIsOk)
            throw new MyValidationException("Проверьте, все ли поля корректно заполнены перед отправкой заявки на проверку.");

        await _repository.SendApplicationAsync(application);
    }

    public async Task<IEnumerable<ApplicationResponse>> GetApplicationIfSubmittedAsync(DateTime? date)
    {
        if (!date.HasValue)
            return Enumerable.Empty<ApplicationResponse>();

        var getSubmittedApplications = await _repository.GetApplicationIfSubmittedAsync(date.Value);
        if (getSubmittedApplications is null)
            throw new MyValidationException($"Нет заявок, удовлетворяющих условию: заявки поданы после {date.Value}");

        var applicationsSubmittedAfteDate = getSubmittedApplications.Select(a => new ApplicationResponse
        {
            Id = a.Id,
            Author = a.Author,
            Activity = a.Activity,
            Name = a.Name,
            Description = a.Description,
            Outline = a.Outline
        });

        return applicationsSubmittedAfteDate;
    }

    public async Task<IEnumerable<ApplicationResponse>> GetUnsobmitedApplicationAsync(DateTime? date)
    {
        if (!date.HasValue)
            return Enumerable.Empty<ApplicationResponse>();

        var getUnsubmittedApplications = await _repository.GetUnsobmitedApplicationAsync(date.Value);
        if (getUnsubmittedApplications is null)
            throw new MyValidationException($"Нет заявок, удовлетворяющих условию: заявки поданы после {date.Value}");

        var applicationsSubmittedAfteDate = getUnsubmittedApplications.Select(a => new ApplicationResponse
        {
            Id = a.Id,
            Author = a.Author,
            Activity = a.Activity,
            Name = a.Name,
            Description = a.Description,
            Outline = a.Outline
        });

        return applicationsSubmittedAfteDate;
    }

    public async Task<ApplicationResponse> GetCurrentUnsubmittedApplicationForUserAsync(Guid id)
    {
        var userExist = await _repository.UserExist(id);
        if (!userExist)
            throw new MyValidationException($"Нет пользователя под идентификатором {id}");

        var application = await _repository.GetCurrentApplication(id);
        if (application is null)
            throw new MyValidationException($"У пользователя нет неотправленных заявок");

        return new ApplicationResponse
        {
            Id = application.Id,
            Author = application.Author,
            Activity = application.Activity,
            Name = application.Name,
            Description = application.Description,
            Outline = application.Outline
        };
    }

    public bool VerificationPropertyAsNullOrEmpty(CreateApplicationRequest application)
    {
        Type properties = application.GetType();
        PropertyInfo[] propertyInfo = properties.GetProperties();
        var values = new List<string>();
        foreach (var item in propertyInfo)
        {
            var value = item.GetValue(application);
            values.Add(value?.ToString() ?? string.Empty);
        }

        foreach (var item in values)
        {
            if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
            {
                return false;
            }
        }
        return true;
    }
    public bool VerificationPropertyAsNullOrEmpty(UserReport application)
    {
        Type properties = application.GetType();
        PropertyInfo[] propertyInfo = properties.GetProperties();
        var values = new List<string>();
        foreach (var item in propertyInfo)
        {
            var value = item.GetValue(application);
            values.Add(value?.ToString() ?? string.Empty);
        }

        foreach (var item in values)
        {
            if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item) || item.Equals("string"))
            {
                return false;
            }
        }
        return true;
    }
}
