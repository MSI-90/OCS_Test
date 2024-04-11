using Application.Dto.Applications;

namespace Api.Orchestrator;

public abstract class HandlerBase<T> where T : class
{
    //protected abstract Task Validate(T request);
    protected abstract Task<IEnumerable<ApplicationResponse>> HandleInner(T request);
    public async Task<IEnumerable<ApplicationResponse>> Handler(T request)
    {
        //Validate(request);
        return await HandleInner(request);
    }
}
