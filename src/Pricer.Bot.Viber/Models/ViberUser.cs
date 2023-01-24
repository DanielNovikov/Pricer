using Newtonsoft.Json;

namespace Pricer.Viber.Models;

public class ViberUser
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("avatar")]
    public string Avatar { get; set; }
    
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("country")]
    public string Country { get; set; }

    [JsonProperty("language")]
    public string Language { get; set; }

    [JsonProperty("api_version")]
    public double ApiVersion { get; set; }
}