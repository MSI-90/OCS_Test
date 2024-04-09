using System.Text.Json.Serialization;

namespace NotionTestWork.Application;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ActivityEnum : byte
{
    Report,
    Masterclass,
    Discussion
}
