using NotionTestWork.Models.DTO_models;
using NotionTestWork.Models.DTO_models.Update;

namespace NotionTestWork.Repositories
{
    public interface IApplication
    {
        Task<ApplicationResponse> CreateApplicationAsync(ApplicationRequest app);
        Task<ApplicationResponse> GetApplicationById(Guid id);
        Task<ApplicationResponse> UpdateApplicationAsync(DataFroUpdateApplication newData, Guid id);
    }
}
