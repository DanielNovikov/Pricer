using Newtonsoft.Json;
using Pricer.Bot.Viber.Models.Enums;

namespace Pricer.Bot.Viber.Models.Message;

public class TextMessage : MessageBase
{
    public TextMessage()
        : base(MessageType.Text)
    {
    }

    private string _text;
    
    [JsonProperty("text")]
    public string Text
    {
        get => _text;
        set => _text = value
            .Replace("<b>", "*")
            .Replace("</b>", "*")
            .Replace("<a href='", "(")
            .Replace("'>", ") ")
            .Replace("</a>", string.Empty);
    }
}