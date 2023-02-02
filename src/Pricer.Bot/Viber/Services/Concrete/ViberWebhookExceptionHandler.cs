using Microsoft.Extensions.Logging;
using Pricer.Bot.Viber.Models;
using Pricer.Bot.Viber.Models.Enums;
using Pricer.Bot.Viber.Models.Message;
using Pricer.Bot.Viber.Services.Abstract;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Service.Abstract;

namespace Pricer.Bot.Viber.Services.Concrete;

public class ViberWebhookExceptionHandler : IViberWebhookHandler
{
    private readonly IViberWebhookHandler _webhookHandler;
    private readonly ILogger<ViberWebhookExceptionHandler> _logger;
    private readonly IResourceService _resourceService;
    private readonly IViberBotService _viberBotService;

    public ViberWebhookExceptionHandler(
        IViberWebhookHandler webhookHandler,
        ILogger<ViberWebhookExceptionHandler> logger, 
        IResourceService resourceService, 
        IViberBotService viberBotService)
    {
        _webhookHandler = webhookHandler;
        _logger = logger;
        _resourceService = resourceService;
        _viberBotService = viberBotService;
    }

    public async Task Handle(ViberWebhookRequest request)
    {
        try
        {
            await _webhookHandler.Handle(request);
        }
        catch (Exception ex)
        {
            if (request.Event != EventType.Message)
            {
                _logger.LogWarning("Unsupported webhook type: {0}", request.Event);
                return;
            }

            if (request.Message is not TextMessage && request.Message is not UrlMessage)
            {
                _logger.LogWarning("Unsupported webhook message type: {0}", request.Event);
                return;
            }

            var text = request.Message switch
            {
                UrlMessage urlMessage => urlMessage.Media,
                TextMessage textMessage => textMessage.Text,
                _ => throw new InvalidOperationException("Couldn't map request to text message")
            };
            
            _logger.LogError(@"User id: {0}
User message: {1}
Exception message: {2}
Inner exception: {3}",
                request.UserId, text, ex.Message, ex.InnerException?.ToString());

            var reply = _resourceService.Get(ResourceKey.Dialog_ErrorOccured);
            await _viberBotService.SendText(request.UserId!, reply);
        }
    }
}