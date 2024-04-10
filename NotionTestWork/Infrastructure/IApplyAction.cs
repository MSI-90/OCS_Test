using Application.Dto.Applications;
using Application.Dto.Applications.CreateApplication;

namespace Api.Infrastructure;

public interface IApplyAction
{
    Task<ApplicationResponse> CreateOrNot(CreateApplicationRequest newApp);
    Task<ApplicationResponse> GetById(Guid applicationId);
    bool VerificationPropertyAsNullOrEmpty(CreateApplicationRequest application);
}
