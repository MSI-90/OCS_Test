using Application.Dto;
using Application.Dto.Applications;
using Application.Interfaces;
using Application.MyException;

namespace Api.Orchestrator;

public class GetDateTimeAsQueryHandlercs(IApplicationService _service) : HandlerBase<ApplicationsFromDateQuery, IEnumerable<ApplicationResponse>>
{
    protected override async Task<IEnumerable<ApplicationResponse>> HandleInner(ApplicationsFromDateQuery request)
    {
        if (request.SubmittedAfter.HasValue && request.UnsubmittedOlder.HasValue)
            throw new MyValidationException("Необходимо указать один из двух параметров");

        if (request.SubmittedAfter.HasValue)
        {
            var result = await _service.GetApplicationIfSubmittedAsync(request.SubmittedAfter.Value);
            return result;
        }
        else if (request.UnsubmittedOlder.HasValue)
        {
            var result = await _service.GetUnsobmitedApplicationAsync(request.UnsubmittedOlder.Value);
            return result;
        }

        return new List<ApplicationResponse>();
    }
}
