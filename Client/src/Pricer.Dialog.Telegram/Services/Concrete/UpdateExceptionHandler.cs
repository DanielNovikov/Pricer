using Microsoft.Extensions.Logging;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Telegram.Services.Abstract;
using Pricer.Telegram.Abstract;
using Telegram.Bot.Types;

namespace Pricer.Dialog.Telegram.Services.Concrete;

public class UpdateExceptionHandler : IUpdateHandler
{
    private readonly IUpdateHandler _handler;
    private readonly ILogger<UpdateExceptionHandler> _logger;
    private readonly ITelegramBotService _telegramBotService;
    private readonly IResourceService _resourceService;

    public UpdateExceptionHandler(
        IUpdateHandler handler,
        ILogger<UpdateExceptionHandler> logger,
        ITelegramBotService telegramBotService, 
        IResourceService resourceService)
    {
        _handler = handler;
        _logger = logger;
        _telegramBotService = telegramBotService;
        _resourceService = resourceService;
    }

    public async Task Handle(Update update)
    {
        try
        {
            await _handler.Handle(update);
        }
        catch (Exception ex)
        {
            var message = update.Message ?? update.CallbackQuery?.Message;
            if (message is null)
            {
                _logger.LogWarning("Unsupported update type: {0}", update.Type);
                return;
            }
            
            var userId = message.Chat.Id;
                
            _logger.LogError(@"User id: {0}
User message: {1}
Message: {2}
InnerException: {3}",
                userId, message.Text, ex.Message, ex.InnerException?.ToString());

            var reply = _resourceService.Get(ResourceKey.Dialog_ErrorOccured);
            await _telegramBotService.SendMessage(userId, reply);
        }
    }
}