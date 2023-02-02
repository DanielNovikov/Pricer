using Newtonsoft.Json;
using Pricer.Bot.Viber.Models.Enums;

namespace Pricer.Bot.Viber.Models.Message;

public class MessageBase
{
    protected MessageBase(MessageType type)
    {
        Type = type;
    }

    [JsonProperty("receiver")]
    public string Receiver { get; set; }

    [JsonProperty("type")]
    public MessageType Type { get; }

    [JsonProperty("sender")]
    public ViberUser Sender { get; set; }

    [JsonProperty("tracking_data")]
    public string TrackingData { get; set; }

    [JsonProperty("min_api_version")]
    public double? MinApiVersion { get; set; }
}