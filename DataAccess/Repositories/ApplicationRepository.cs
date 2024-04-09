using Application.Dto.Applications;
using Application.Dto.Applications.CreateApplication;
using Application.Dto.Applications.UpdateApplication;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TestWorkForNotion.DataAccess;


namespace NotionTestWork.DataAccess.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly ApplicationContext _context;
    public ApplicationRepository(ApplicationContext context) => _context = context;

    //Добавить новую заявку
    public async Task<ApplicationResponse> CreateApplicationAsync(CreateApplicationRequest app)
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

    //Получить заявку по Id
    public async Task<ApplicationResponse> GetApplicationById(Guid id)
    {
        /*
        var application = await _context.applications.Include(a => a.Author).SingleOrDefaultAsync(a => a.Id == id);
        if (application != null && !string.IsNullOrEmpty(application.Name))
        {
            return new ApplicationResponse
            {
                Id = application.Id,
                Author = application.Author.Id,
                Activity = application.Activity,
                Name = application.Name,
                Description = application.Description,
                Outline = application.Outline
            };
        }

        return new ApplicationResponse();
        */
        return null;
    }

    //отредактировать заявку
    public async Task<ApplicationResponse> UpdateApplicationAsync(UpdateApplicationRequest newData, Guid id)
    {
        /*
        var applicationFrorUpdate = await _context.applications.Include(a => a.Author).SingleOrDefaultAsync(a => a.Id == id);
        if (applicationFrorUpdate.IsSubmitted == true)
            throw new Exception("Данная заявка не может быть отредактирована, потому, что она была уже отправлена на проверку");

        if (applicationFrorUpdate != null)
        {
            applicationFrorUpdate.Activity = newData.Activity;
            applicationFrorUpdate.Name = string.IsNullOrEmpty(newData.Name) ? applicationFrorUpdate.Name : newData.Name;
            applicationFrorUpdate.Description = string.IsNullOrEmpty(newData.Description) ? applicationFrorUpdate.Description : newData.Description;
            applicationFrorUpdate.Outline = string.IsNullOrEmpty(newData.Outline) ? applicationFrorUpdate.Outline : newData.Outline;
            applicationFrorUpdate.CreatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ApplicationResponse
            {
                Id = applicationFrorUpdate.Id,
                Author = applicationFrorUpdate.Author.Id,
                Activity = applicationFrorUpdate.Activity,
                Name = applicationFrorUpdate.Name,
                Description = applicationFrorUpdate.Description,
                Outline = applicationFrorUpdate.Outline
            };
        }

        return new ApplicationResponse();
        */
        return null;
    }

    //удалить заявку по Id
    public async Task DeleteApplicationById(Guid id)
    {
        /*
        var application = await _context.applications.SingleOrDefaultAsync(a => a.Id == id);
        if (application is not null && application.IsSubmitted == false)
        {
            _context.applications.Remove(application);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new Exception("Заявка была отправлена на проверку и не может быть удалена");
        }
        */
    }

    //отправить заявку на проверку программным комитетом
    public async Task SendApplicationAsync(Guid id)
    {
        /*
        var application = await _context.applications.SingleOrDefaultAsync(application => application.Id == id);

        if (application.IsSubmitted == true)
            throw new Exception("Данная заявка уже была отправлена на проверку ранее");

        Type propertyAsList = application.GetType();
        PropertyInfo[] properties = propertyAsList.GetProperties();
        var values = new List<string>();

        foreach (var item in properties)
            values.Add(item.GetValue(application)?.ToString() ?? string.Empty);

        foreach (var item in values)
        {
            if (string.IsNullOrEmpty(item) || item.Equals("string"))
            {
                throw new Exception("Приведите заявку в корректный вид (не заполнены или некорректно заполнены поля), и после этого можете поаторить отправку");
            }
        }

        application.IsSubmitted = true;
        await _context.SaveChangesAsync();
        */
    }

    //получаем отправленные заявки поданые после указаной даты
    public async Task<IEnumerable<ApplicationResponse>> GetApplicationIfSubmittedAsync(DateTime date)
    {
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
        */
        return null;
    }

    //получаем не поданые заявки и старше указаной даты
    public async Task<IEnumerable<ApplicationResponse>> GetUnsobmitedApplicationAsync(DateTime date)
    {
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
        return null;
    }

    public async Task<ApplicationResponse> GetCurrentApplication(Guid id)
    {
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
        return null;
    }
}
