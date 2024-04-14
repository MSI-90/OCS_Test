using Application.Dto.Applications;
using Application.Interfaces;

namespace Api.Orchestrator;

public abstract class HandlerBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    protected abstract Task<TResponse> HandleInner(TRequest request);
    public async Task<TResponse> Handle(TRequest request)
    {
        return await HandleInner(request);
    }
}
