using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Pricer.Viber.Models.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum MessageType
{
    [EnumMember(Value = "text")]
    Text = 1,
    
    [EnumMember(Value = "picture")]
    Picture = 2,

    [EnumMember(Value = "video")]
    Video = 3,

    [EnumMember(Value = "file")]
    File = 4,

    [EnumMember(Value = "location")]
    Location = 5,

    [EnumMember(Value = "contact")]
    Contact = 6,

    [EnumMember(Value = "sticker")]
    Sticker = 7,

    [EnumMember(Value = "rich_media")]
    CarouselContent = 8,

    [EnumMember(Value = "url")]
    Url = 9
}