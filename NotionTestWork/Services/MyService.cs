using Helpers;
using Microsoft.OpenApi.Validations;
using NotionTestWork.Models;

namespace NotionTestWork.Services
{
    public class MyService
    {
        private readonly int ActivitiValues = 0;
        ActivityHelper helper;
        public MyService()
        {
            ActivitiValues = Enum.GetValues(typeof(ActivityEnum)).Length;
        }

        public IEnumerable<ActivitiesResponse> GetActivities()
        {
            helper = new ActivityHelper();
            var activitiValues = new List<ActivityEnum>(Enum.GetValues(typeof(ActivityEnum)).Cast<ActivityEnum>());
            var activityDescriptions = new Dictionary<string, string>();

            for (byte i = 0; i < ActivitiValues; i++)
            {
                activityDescriptions.Add(activitiValues[i].ToString(), helper.GetPriorityDescription(i));
            }

            var activitiResponse = activityDescriptions.Select(ad => new ActivitiesResponse
            {
                Activity = ad.Key,
                ActivityDescription = ad.Value,
            });

            return activitiResponse;
        }
    }
}
