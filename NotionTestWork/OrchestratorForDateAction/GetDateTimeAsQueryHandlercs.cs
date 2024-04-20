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

        var result = request.SubmittedAfter.HasValue
        ? await _service.GetApplicationIfSubmittedAsync(request.SubmittedAfter.Value)
        : request.UnsubmittedOlder.HasValue
            ? await _service.GetUnsobmitedApplicationAsync(request.UnsubmittedOlder.Value)
            : new List<ApplicationResponse>();

        return result;
    }
}
