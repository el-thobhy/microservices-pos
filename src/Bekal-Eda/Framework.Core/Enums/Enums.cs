using System.Text.Json.Serialization;

namespace Framework.Core.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AttributeTypeEnum
    {
        Text,
        Number,
        Decimal
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RecordStatusEnum
    {
        Active,
        Inactive,
        Removed
    }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserTypeEnum
    {
        Customer,
        Administrator
    }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CartStatusEnum
    {
        Pending,
        Confirmed,
        Paid,
        Canceled
    }
}