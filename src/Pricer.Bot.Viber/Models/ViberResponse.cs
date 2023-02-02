using Newtonsoft.Json;
using Pricer.Viber.Models.Enums;

namespace Pricer.Viber.Models;

public class ViberResponse
{
    [JsonProperty("status")]
    public ViberResponseCode Status { get; set; }

    [JsonProperty("status_message")]
    public string? StatusMessage { get; set; }
}