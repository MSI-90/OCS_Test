using System.Text.Json.Serialization;

namespace NotionTestWork.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ActivityEnum : byte
    {
        Report,
        Masterclass,
        Discussion
    }
}
