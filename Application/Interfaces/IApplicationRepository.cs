using NotionTestWork.Domain.Models;

namespace NotionTestWork.DataAccess.Repositories;
public interface IApplicationRepository
{
    Task<bool> ApplicationExistForUserAsync(Guid userId);
    Task CreateApplication(UserReport createApplication);
    Task<UserReport> GetApplicationById(Guid id);
    //Task<ApplicationResponse> UpdateApplicationAsync(UpdateApplicationRequest newData, Guid id);
    //Task DeleteApplicationById(Guid id);
    //Task SendApplicationAsync(Guid id);
    //Task<IEnumerable<ApplicationResponse>> GetApplicationIfSubmittedAsync(DateTime date);
    //Task<IEnumerable<ApplicationResponse>> GetUnsobmitedApplicationAsync(DateTime date);
    //Task<ApplicationResponse> GetCurrentApplication(Guid id);
}
