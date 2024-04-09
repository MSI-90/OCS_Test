using Application.Dto.Applications;
using Application.Dto.Applications.CreateApplication;
using Application.Dto.Applications.UpdateApplication;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using NotionTestWork.DataAccess.Repositories;
using NotionTestWork.Domain.Models;

namespace Application.Services.Application;
public class ApplicationService : IApplicationService
{
    private readonly IApplicationDbContext _context;
    private readonly IApplicationRepository _repository;
    public ApplicationService(IApplicationDbContext context, IApplicationRepository repository)
    {
        _context = context;
        _repository = repository;
    }
    public async Task<ApplicationResponse> CreateApplicationAsync(CreateApplicationRequest app)
    {
        bool applicationAsUnsubmitForUserExist = await _repository.ApplicationExistForUserAsync(app.Author);
        if (applicationAsUnsubmitForUserExist)
            throw new Exception($"У Вас уже имеется заявка в статусе - не отправлена");

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

        _repository.CreateApplication(newApplicationToDb);
        await _context.SaveChangesAsync();

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

    public Task<ApplicationResponse> CreateApplicationAsync(CreateApplicationRequest app, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task DeleteApplicationById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationResponse> GetApplicationById(Guid id)
    {
        throw new NotImplementedException();
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
