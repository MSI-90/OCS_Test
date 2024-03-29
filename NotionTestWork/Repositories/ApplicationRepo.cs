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
            //Guid.TryParse(id, out Guid guid);
            //if (guid == default)
            //    throw new Exception("Нет заявки с указаным идентификатором.");

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
            //Guid.TryParse(id, out Guid guidFrom);
            //if (guidFrom == default)
            //    throw new Exception("неверный параметр в качестве идентификатора");

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
    }
}
