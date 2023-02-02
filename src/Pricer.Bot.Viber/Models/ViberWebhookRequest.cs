using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pricer.Dialog.Models;
using Pricer.Viber.Models.Enums;
using Pricer.Viber.Models.Message;

namespace Pricer.Viber.Models;

public class ViberWebhookRequest
{
    [JsonProperty("event")]
    public EventType Event { get; set; }

    [JsonProperty("message_token")]
    public long MessageToken { get; set; }

    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }

    [JsonProperty("user_id")]
    public string? UserId { get; set; }

    [JsonProperty("user")]
    public ViberUser? User { get; set; }

    [JsonProperty("subscribed")]
    public bool? Subscribed { get; set; }

    [JsonProperty("context")]
    public string? Context { get; set; }

    [JsonProperty("desc")]
    public string? Description { get; set; }

    [JsonProperty("sender")]
    public ViberUser? Sender { get; set; }
    
    [JsonIgnore]
    public MessageBase? Message { get; set; }

    [JsonProperty("message")]
    private JObject RawMessage
    {
        set
        {
            var messageType = value.Property("type")!.Value.ToObject<MessageType>();

            var type = messageType switch
            {
                MessageType.Text => typeof(TextMessage),
                MessageType.Url => typeof(UrlMessage),
                _ => default
            };

            if (type != null)
                Message = (MessageBase)value.ToObject(type)!;
        }
    }

    public MessageHandlingModel MapToMessage()
    {
        if (Message is not TextMessage textMessage)
            throw new InvalidOperationException("Couldn't map request to text message");
        
        return new MessageHandlingModel(textMessage.Text, MapUser());
    }

    public CallbackHandlingModel MapToCallback()
    {
        return Message switch
        {
            UrlMessage urlMessage => new CallbackHandlingModel(urlMessage.Media, default, MapUser()),
            TextMessage textMessage => new CallbackHandlingModel(textMessage.Text, default, MapUser()),
            _ => throw new InvalidOperationException("Couldn't map request to text message")
        };
    }

    private UserModel MapUser()
    {
        if (Sender is null)
            throw new InvalidOperationException("Couldn't map sender");
        
        return new UserModel(
            Sender.Id,
            Sender.Name,
            default);
    }
}

