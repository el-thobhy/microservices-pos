using System.Text.Json.Serialization;

namespace LookUp.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AttributeTypeEnum
    {
        Text,
        Number,
        Decimal
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LookUpStatusEnum
    {
        Active,
        Inactive,
        Removed
    }
}