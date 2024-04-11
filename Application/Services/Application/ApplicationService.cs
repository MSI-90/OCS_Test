using Application.Dto.Applications;
using Application.Dto.Applications.CreateApplication;
using Application.Dto.Applications.UpdateApplication;
using Application.Interfaces;
using Application.MyException;
using NotionTestWork.DataAccess.Repositories;
using NotionTestWork.Domain.Models;

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

        //var application = await _context.applications.SingleOrDefaultAsync(a => a.Id == id);
        //if (application is not null && application.IsSubmitted == false)
        //{
        //    _context.applications.Remove(application);
        //    await _context.SaveChangesAsync();
        //}
        //else
        //{
        //    throw new Exception("Заявка была отправлена на проверку и не может быть удалена");
        //}

    }

    public async Task<ApplicationResponse> GetApplicationById(Guid id)
    {
        var applicatoinFromRepository = await _repository.GetApplicationById(id);
        if (applicatoinFromRepository != null && !string.IsNullOrEmpty(applicatoinFromRepository.Name))
        {
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
        return new ApplicationResponse();
    }

    public Task<IEnumerable<ApplicationResponse>> GetApplicationIfSubmittedAsync(DateTime date)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationResponse> GetCurrentApplication(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ApplicationResponse>> GetUnsobmitedApplicationAsync(DateTime date)
    {
        throw new NotImplementedException();
    }

    public Task SendApplicationAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationResponse> UpdateApplicationAsync(UpdateApplicationRequest newData, Guid id)
    {
        throw new NotImplementedException();
    }
}
