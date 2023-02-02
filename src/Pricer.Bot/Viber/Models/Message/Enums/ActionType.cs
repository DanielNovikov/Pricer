using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pricer.Bot.Viber.Models.Message.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum ActionType
{
    [EnumMember(Value = "none")]
    None = 1,
    
    [EnumMember(Value = "open-url")]
    OpenUrl = 2,
    
    [EnumMember(Value = "reply")]
    Reply = 3
}