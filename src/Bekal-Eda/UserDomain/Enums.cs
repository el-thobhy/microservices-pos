using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace User.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserTypeEnum
    {
        Customer,
        Administrator   
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RecordStatusEnum
    {
        Active,
        Inactive,
        Removed
    }
}
