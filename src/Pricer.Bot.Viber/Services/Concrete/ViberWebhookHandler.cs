using Newtonsoft.Json;
using Pricer.Bot.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;
using Pricer.Viber.Models;
using Pricer.Viber.Models.Enums;
using Pricer.Viber.Models.Message;
using Pricer.Viber.Services.Abstract;

namespace Pricer.Viber.Services.Concrete;

public class ViberWebhookHandler : IViberWebhookHandler
{
    private readonly IViberMessageHandler _viberMessageHandler;
    private readonly IViberCallbackHandler _callbackHandler;

    public ViberWebhookHandler(
        IViberMessageHandler viberMessageHandler, 
        IViberCallbackHandler callbackHandler)
    {
        _viberMessageHandler = viberMessageHandler;
        _callbackHandler = callbackHandler;
    }

    public async Task Handle(ViberWebhookRequest request)
    {
        if (request.Event == EventType.Message)
        {
            if (request.Message is TextMessage textMessage)
            {
                var isTextJson = true;
                var settings = new JsonSerializerSettings
                {
                    Error = (sender, args) =>
                    {
                        isTextJson = false;
                        args.ErrorContext.Handled = true;
                    },
                    MissingMemberHandling = MissingMemberHandling.Error
                };
                
                JsonConvert.DeserializeObject<CallbackData>(textMessage.Text, settings);
                if (isTextJson)
                {
                    await _callbackHandler.Handle(request.MapToCallback());
                }
                else
                {
                    await _viberMessageHandler.Handle(request.MapToMessage());
                }
            }
        }
    }
}