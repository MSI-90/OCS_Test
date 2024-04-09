using Application.Dto.Activity;
using Application.Interfaces;
using NotionTestWork.Application;

namespace Application.Services.Activities;

public class ActivitiesService : IActivities
{
    private readonly int ActivitiValues;
    public ActivitiesService()
    {
        ActivitiValues = Enum.GetValues(typeof(ActivityEnum)).Length;
    }

    public IEnumerable<ActivitiesResponse> GetActivities()
    {
        string[] descriptionVariants = ["Доклад, 35-45 минут", "Мастеркласс, 1-2 часа", "Дискуссия/круглый стол, 40-50 минут"];
        var activitiValues = new List<ActivityEnum>(Enum.GetValues(typeof(ActivityEnum)).Cast<ActivityEnum>());
        var activityDescriptions = new Dictionary<string, string>();

        for (byte i = 0; i < ActivitiValues; i++)
        {
            activityDescriptions.Add(activitiValues[i].ToString(), descriptionVariants[i]);
        }

        var activitiResponse = activityDescriptions.Select(ad => new ActivitiesResponse
        {
            Activity = ad.Key,
            ActivityDescription = ad.Value,
        });

        return activitiResponse;
    }
}
