using Application.Dto.Create;
using NotionTestWork.Application.Dto;
using NotionTestWork.Application.Dto.Update;

namespace NotionTestWork.DataAccess.Repositories;

public interface IApplication
{
    Task<ApplicationResponse> CreateApplicationAsync(CreateApplicationRequest app);
    Task<ApplicationResponse> GetApplicationById(Guid id);
    Task<ApplicationResponse> UpdateApplicationAsync(UpdateApplicationRequest newData, Guid id);
    Task DeleteApplicationById(Guid id);
    Task SendApplicationAsync(Guid id);
    Task<IEnumerable<ApplicationResponse>> GetApplicationIfSubmittedAsync(DateTime date);
    Task<IEnumerable<ApplicationResponse>> GetUnsobmitedApplicationAsync(DateTime date);
    Task<ApplicationResponse> GetCurrentApplication(Guid id);
}
