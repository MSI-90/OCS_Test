using Application.Dto.Applications;
using Application.Dto.Applications.CreateApplication;
using Application.Dto.Applications.UpdateApplication;
using Application.Interfaces;

namespace Application.Services.Application;
public class ApplicationService : IApplicationService
{
    private readonly IApplicationDbContext _context;
    public Task<ApplicationResponse> CreateApplicationAsync(CreateApplicationRequest app)
    {
        //var user = await _context.users.SingleOrDefaultAsync(u => u.Id == app.Author);
        //if (user == null)
        //    throw new Exception("Пользователь не нейден");
        /*
        var appTitle = await _context.applications.Where(a => a.Author == user)
            .FirstOrDefaultAsync(a => a.Name == app.Name);

        if (appTitle != null)
            throw new Exception($"Уже имеется заявка, именуемая, как {app.Name}");

        var isDraftApplication = await _context.applications.Include(a => a.Author).FirstOrDefaultAsync(a => a.IsSubmitted == false && a.Author.Id == user.Id);
        if (isDraftApplication != null)
            throw new Exception($"У Вас уже имеется заявка в статусе - не отправлена, идентификатор заявки - {isDraftApplication.Id}");

        var newApplicationToDb = new Application
        {
            Id = Guid.NewGuid(),
            Author = user,
            Activity = app.Activity,
            CreatedAt = DateTime.UtcNow,
            Name = app.Name,
            Description = app.Description,
            Outline = app.Outline,
            IsSubmitted = false
        };

        await _context.applications.AddAsync(newApplicationToDb);
        await _context.SaveChangesAsync();

        var newApplicationResponse = new ApplicationResponse
        {
            Id = newApplicationToDb.Id,
            Author = user.Id,
            Activity = newApplicationToDb.Activity,
            Name = newApplicationToDb.Name,
            Description = newApplicationToDb.Description,
            Outline = newApplicationToDb.Outline
        };
         return newApplicationResponse;
        */
        return null;
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
