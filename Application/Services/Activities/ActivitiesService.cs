using Application.Dto.Activity;
using Application.Interfaces;
using Application.MyException;
using System.Net;

namespace Application.Services.Activities;

public class ActivitiesService(IActivityRepository _repository) : IActivityService
{
    public async Task<IEnumerable<ActivitiesResponse>> GetActivitiesAsync()
    {
        var list = await _repository.GetActivityTypesAsync();
        if (!list.Any())
            throw new MyValidationException("Нет данных для отображения", HttpStatusCode.NotFound);

        return list.Select(a => new ActivitiesResponse
        {
            Activity = a.TypeOfActivity,
            Description = a.Description,
        });
    }
}
