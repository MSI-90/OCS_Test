using NotionTestWork.Models.DTO_models;
using NotionTestWork.Models.DTO_models.Update;

namespace NotionTestWork.Repositories
{
    public interface IApplication
    {
        Task<ApplicationResponse> CreateApplicationAsync(ApplicationRequest app);
        Task<ApplicationResponse> GetApplicationById(Guid id);
        Task<ApplicationResponse> UpdateApplicationAsync(DataFroUpdateApplication newData, Guid id);
        Task DeleteApplicationById(Guid id);
        Task SendApplicationAsync(Guid id);
        Task<IEnumerable<ApplicationResponse>> GetApplicationIfSubmittedAsync(DateTime date);
        Task<IEnumerable<ApplicationResponse>> GetUnsobmitedApplicationAsync(DateTime date);
        Task<ApplicationResponse> GetCurrentApplication(Guid id);
    }
}
