using Newtonsoft.Json;

namespace Pricer.Bot.Viber.Models.Message;

public class KeyboardMessage : TextMessage
{
    [JsonProperty("keyboard")]
    public Keyboard Keyboard { get; set; } = default!;
}

public record Keyboard(KeyboardButton[] Buttons)
{
    public string Type => "keyboard";
};

public class KeyboardButton
{
    public int Rows => 1;

    public int Columns { get; set; }

    private string _text;
    public string Text
    {
        get => _text;
        set => _text = $"<font color=\"#e6e6e6\">{value}</font>";
    }

    public string ActionBody { get; set; } = default!;
    
    public string BgColor => "#2847fa";
}