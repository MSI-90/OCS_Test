using System.Text.Json.Serialization;

namespace NotionTestWork.Domain;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ActivityEnum : byte
{
    Report,
    Masterclass,
    Discussion
}
