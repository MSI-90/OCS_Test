﻿using Microsoft.AspNetCore.Mvc;
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

        //Добавить новую заявку
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

            var IsDraftApplication = await _context.applications.Include(a => a.Author).FirstOrDefaultAsync(a => a.IsSubmitted == false && a.Author.Id == user.Id);
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
                Outline = app.Outline,
                IsSubmitted = false
            };

            await _context.applications.AddAsync(newApplicationToDb);
            await _context.SaveChangesAsync();

            return newApplicationResponse;
        }

        //Получить заявку по Id
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

        //отредактировать заявку
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

        //удалить заявку по Id
        public async Task DeleteApplicationById(Guid id)
        {
            var application = await _context.applications.SingleOrDefaultAsync(a => a.Id == id);
            if (application is not null)
            {
                _context.applications.Remove(application);
                await _context.SaveChangesAsync();
            }
        }

        //отправить щаявку на проверку программным комитетом
        public async Task SendApplicationAsync(Guid id)
        {
            var application = await _context.applications.SingleOrDefaultAsync(application => application.Id == id);
            application.IsSubmitted = true;
            await _context.SaveChangesAsync();
        }

        //получаем отправленные заявки поданые после указаной даты
        public async Task<IEnumerable<ApplicationResponse>> GetApplicationIfSubmittedAsync(DateTime date)
        {
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
        }

        //получаем не поданые заявки и старше указаной даты
        public async Task<IEnumerable<ApplicationResponse>> GetUnsobmitedApplicationAsync(DateTime date)
        {
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
        }
    }
}
