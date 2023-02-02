using Newtonsoft.Json;
using Pricer.Bot.Viber.Models.Enums;

namespace Pricer.Bot.Viber.Models;

public class ViberResponse
{
    [JsonProperty("status")]
    public ViberResponseCode Status { get; set; }

    [JsonProperty("status_message")]
    public string? StatusMessage { get; set; }
}