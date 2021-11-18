using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PriceObserver.Telegram.Abstract;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Concrete
{
    public class UpdateExceptionHandler : IUpdateHandler
    {
        private readonly IUpdateHandler _handler;
        private readonly ILogger<UpdateExceptionHandler> _logger;
        private readonly ITelegramBotService _telegramBotService;

        public UpdateExceptionHandler(
            IUpdateHandler handler,
            ILogger<UpdateExceptionHandler> logger,
            ITelegramBotService telegramBotService)
        {
            _handler = handler;
            _logger = logger;
            _telegramBotService = telegramBotService;
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
Message: {ex.Message}
InnerException: {ex.InnerException}");

                await _telegramBotService.SendMessage(userId, "Произошла ошибка ❌");
            }
        }
    }
}