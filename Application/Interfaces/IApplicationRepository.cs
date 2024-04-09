using Application.Dto.Applications;
using Application.Dto.Applications.CreateApplication;
using Application.Dto.Applications.UpdateApplication;

namespace NotionTestWork.DataAccess.Repositories;

public interface IApplicationRepository
{
    Task<bool> ApplicationExistForUser(Guid userId);
    //Task<ApplicationResponse> GetApplicationById(Guid id);
    //Task<ApplicationResponse> UpdateApplicationAsync(UpdateApplicationRequest newData, Guid id);
    //Task DeleteApplicationById(Guid id);
    //Task SendApplicationAsync(Guid id);
    //Task<IEnumerable<ApplicationResponse>> GetApplicationIfSubmittedAsync(DateTime date);
    //Task<IEnumerable<ApplicationResponse>> GetUnsobmitedApplicationAsync(DateTime date);
    //Task<ApplicationResponse> GetCurrentApplication(Guid id);
}
