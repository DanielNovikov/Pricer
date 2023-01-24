using Newtonsoft.Json;
using Pricer.Viber.Models.Enums;

namespace Pricer.Viber.Models.Message;

public class TextMessage : MessageBase
{
    public TextMessage()
        : base(MessageType.Text)
    {
    }

    [JsonProperty("text")]
    public string Text { get; set; } = default!;
}