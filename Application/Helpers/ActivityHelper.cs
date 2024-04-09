using NotionTestWork.Models;

namespace Helpers;

internal class ActivityHelper
{
    private string DescriptionVariants(byte id)
    {
        string[] descriptionVariants = ["Доклад, 35-45 минут", "Мастеркласс, 1-2 часа", "Дискуссия / круглый стол, 40-50 минут"];

        return descriptionVariants[id];
    }

    public string GetPriorityDescription(byte digit)
    {
        return DescriptionVariants((byte)digit);
    }
}
