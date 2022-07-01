using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Telegram.Abstract;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Concrete;

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
            var userId = update.Message.Chat.Id;
                
            _logger.LogError($@"User id: {userId}
User message: {update.Message.Text}
Message: {ex.Message}
InnerException: {ex.InnerException}");

            var message = _resourceService.Get(ResourceKey.Dialog_ErrorOccured);
            await _telegramBotService.SendMessage(userId, message);
        }
    }
}