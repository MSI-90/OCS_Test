using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotionTestWork.Models.DTO_models;
using NotionTestWork.Models.DTO_models.Update;
using NotionTestWork.Models.EfClasses;
using TestWorkForNotion.EfCode;

namespace NotionTestWork.Repositories
{
    public class ApplicationRepo : IApplication
    {
        private readonly NutchellContext _context;
        public ApplicationRepo(NutchellContext context) => _context = context;

        public async Task<ApplicationResponse> CreateApplicationAsync(ApplicationRequest app)
        {
            var user = await _context.users.SingleOrDefaultAsync(u => u.Id == app.Author);
            if (user == null)
                throw new Exception("Пользователь не нейден");

            var appTitle = await _context.applications
                .Where(a => a.Author == user)
                .FirstOrDefaultAsync(a => a.Name == app.Name);

            if (appTitle != null)
                throw new Exception($"Уже имеется заявка, именуемая, как {app.Name}");

            var IsDraftApplication = await _context.applications.FirstOrDefaultAsync(a => a.IsSubmitted == false);
            if (IsDraftApplication != null)
                throw new Exception($"У Вас уже имеется заявка в статусе - не отправлена, идентификатор заявки - {IsDraftApplication.Id}");

            var newApplicationResponse = new ApplicationResponse
            {
                Id = Guid.NewGuid(),
                Author = app.Author,
                Activity = app.Activity,
                Name = app.Name,
                Description = app.Description,
                Outline = app.Outline
            };

            var newApplicationToDb = new Application
            {
                Id = newApplicationResponse.Id,
                Author = user,
                Activity = newApplicationResponse.Activity,
                CreatedAt = DateTime.UtcNow,
                Name = app.Name,
                Description = app.Description,
                Outline = app.Outline
            };

            await _context.applications.AddAsync(newApplicationToDb);
            await _context.SaveChangesAsync();

            return newApplicationResponse;
        }

        public async Task<ApplicationResponse> GetApplicationById(Guid id)
        {
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
        }

        public async Task<ApplicationResponse> UpdateApplicationAsync(DataFroUpdateApplication newData, Guid id)
        {
            var applicationFrorUpdate = await _context.applications.Include(a => a.Author).SingleOrDefaultAsync(a => a.Id == id);
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
        }

        public async Task DeleteApplicationById(Guid id)
        {
            var application = await _context.applications.SingleOrDefaultAsync(a => a.Id == id);
            if (application is not null)
            {
                _context.applications.Remove(application);
                await _context.SaveChangesAsync();
            }
        }

        //send application to comitet of programming
        public async Task SendApplicationAsync(Guid id)
        {
            var application = await _context.applications.SingleOrDefaultAsync(application => application.Id == id);
            application.IsSubmitted = true;
            await _context.SaveChangesAsync();
        }
    }
}
