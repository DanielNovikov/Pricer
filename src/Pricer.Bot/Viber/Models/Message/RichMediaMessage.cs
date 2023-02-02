using Newtonsoft.Json;
using Pricer.Bot.Viber.Models.Enums;
using Pricer.Bot.Viber.Models.Message.Enums;

namespace Pricer.Bot.Viber.Models.Message;

public class RichMediaMessage : MessageBase
{
    public RichMediaMessage() : base(MessageType.CarouselContent)
    {
    }
    
    [JsonProperty("rich_media")]
    public RichMedia RichMedia { get; set; }
}

public class RichMedia
{
    public string Type => "rich_media";

    public int ButtonsGroupColumns => 6;

    public int ButtonsGroupRows { get; set; }

    public string BgColor => "#FFFFFF";

    public RichMediaButton[] Buttons { get; set; } = default!;
}

public class RichMediaButton
{
    public int Columns { get; set; }

    public int Rows { get; set; } = 1;

    public ActionType ActionType { get; set; }

    public string? ActionBody { get; set; }

    private string? _text;
    public string? Text
    {
        get => _text;
        set => _text = ActionType == ActionType.None ? value : $"<font color=\"#e6e6e6\">{value}</font>";
    }

    public string BgColor => ActionType == ActionType.None ? "#ffffff" : "#2847fa";
}