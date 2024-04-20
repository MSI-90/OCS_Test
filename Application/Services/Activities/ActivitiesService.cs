using Application.Dto.Activity;
using Application.Interfaces;
using Application.MyException;
using System.Net;

namespace Application.Services.Activities;

public class ActivitiesService(IActivityRepository _repository) : IActivityService
{
    public IEnumerable<ActivitiesResponse> GetActivities()
    {
        var list = _repository.GetActivityTypes();
        if (!list.Any())
            throw new MyValidationException("Нет данных для отображения", HttpStatusCode.NotFound);

        return list.Select(a => new ActivitiesResponse
        {
            Activity = a.TypeOfActivity,
            Description = a.Description,
        });
    }
}
