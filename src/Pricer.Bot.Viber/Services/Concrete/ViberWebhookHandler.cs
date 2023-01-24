using Pricer.Bot.Abstract;
using Pricer.Dialog.Services.Abstract;
using Pricer.Viber.Models;
using Pricer.Viber.Models.Enums;
using Pricer.Viber.Services.Abstract;

namespace Pricer.Viber.Services.Concrete;

public class ViberWebhookHandler : IViberWebhookHandler
{
    private readonly IBotMessageHandler _messageHandler;
    private readonly IViberBotService _viberBotService;

    public ViberWebhookHandler(
        IBotMessageHandler messageHandler,
        IViberBotService viberBotService)
    {
        _messageHandler = messageHandler;
        _viberBotService = viberBotService;
    }

    public async Task Handle(ViberWebhookRequest request)
    {
        if (request.Event == EventType.Message)
        {
            await _messageHandler.Handle(request.MapToMessage(), _viberBotService);
            return;
        }
    }
}