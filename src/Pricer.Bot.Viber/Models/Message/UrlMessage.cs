using Newtonsoft.Json;
using Pricer.Viber.Models.Enums;

namespace Pricer.Viber.Models.Message;

public class UrlMessage : MessageBase
{
    public UrlMessage()
        : base(MessageType.Url)
    {
    }

    [JsonProperty("media")]
    public string Media { get; set; } = default!;
}