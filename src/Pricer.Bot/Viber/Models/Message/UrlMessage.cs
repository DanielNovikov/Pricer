using Newtonsoft.Json;
using Pricer.Bot.Viber.Models.Enums;

namespace Pricer.Bot.Viber.Models.Message;

public class UrlMessage : MessageBase
{
    public UrlMessage()
        : base(MessageType.Url)
    {
    }

    [JsonProperty("media")]
    public string Media { get; set; } = default!;
}