using Pricer.Bot.Abstract;
using Pricer.Dialog.Services.Abstract;
using Pricer.Viber.Models;
using Pricer.Viber.Models.Enums;
using Pricer.Viber.Services.Abstract;

namespace Pricer.Viber.Services.Concrete;

public class ViberWebhookHandler : IViberWebhookHandler
{
    private readonly IViberMessageHandler _viberMessageHandler;
    private readonly IViberBotService _viberBotService;

    public ViberWebhookHandler(
        IViberBotService viberBotService, 
        IViberMessageHandler viberMessageHandler)
    {
        _viberBotService = viberBotService;
        _viberMessageHandler = viberMessageHandler;
    }

    public async Task Handle(ViberWebhookRequest request)
    {
        if (request.Event == EventType.Message)
        {
            await _viberMessageHandler.Handle(request.MapToMessage());
            return;
        }
    }
}