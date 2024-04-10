using Api.Middlewares.ExceptionMiddleware;
using Application.Dto.Applications;
using Application.Dto.Applications.CreateApplication;
using Application.Interfaces;
using System.Net;
using System.Reflection;

namespace Api.Infrastructure;

public class ApplyAction : IApplyAction
{
    private readonly IApplicationService _service;
    public ApplyAction(IApplicationService service) => _service = service;
    public async Task<ApplicationResponse> CreateOrNot(CreateApplicationRequest newApp)
    {
        var stringIsOk = VerificationPropertyAsNullOrEmpty(newApp);
        if (!stringIsOk)
            throw new MyValidationException("Ошибка при создании заявки, проверьте достоверность заполнения полей перед созданием заявки.");

        var response = await _service.CreateApplicationAsync(newApp);
        return response;
    }

    public async Task<ApplicationResponse> GetById(Guid applicationId)
    {
        var response = await _service.GetApplicationById(applicationId);
        if (string.IsNullOrEmpty(response.Name))
        {
            throw new MyValidationException($"Нет заявки под id = {applicationId}", HttpStatusCode.NotFound);
        }
        return response;
    }

    public bool VerificationPropertyAsNullOrEmpty(CreateApplicationRequest application)
    {
        Type properties = application.GetType();
        PropertyInfo[] propertyInfo = properties.GetProperties();
        var values = new List<string>();
        foreach (var item in propertyInfo)
        {
            var value = item.GetValue(application);
            values.Add(value?.ToString() ?? string.Empty);
        }

        foreach (var item in values)
        {
            if (string.IsNullOrEmpty(item) || string.IsNullOrWhiteSpace(item))
            {
                return false;
            }
        }
        return true;
    }
}
