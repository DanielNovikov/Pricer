using System.Threading.Tasks;
using PriceObserver.Dialog.Input.Abstract;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Telegram.Abstract;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Concrete
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly IUpdateToUpdateDtoConverter _updateDtoConverter;
        private readonly IInputHandler _inputHandler;
        private readonly ITelegramBotService _telegramBotService;
        private readonly IReplyKeyboardMarkupBuilder _keyboardBuilder;
        
        public UpdateHandler(
            IUpdateToUpdateDtoConverter updateDtoConverter,
            IInputHandler inputHandler, 
            ITelegramBotService telegramBotService,
            IReplyKeyboardMarkupBuilder keyboardBuilder)
        {
            _updateDtoConverter = updateDtoConverter;
            _inputHandler = inputHandler;
            _telegramBotService = telegramBotService;
            _keyboardBuilder = keyboardBuilder;
        }

        public async Task Handle(Update update)
        {
            var updateDto = _updateDtoConverter.Convert(update);
            var userId = updateDto.UserId;

            var serviceResult = await _inputHandler.Handle(updateDto);

            if (!serviceResult.IsSuccess)
            {
                await _telegramBotService.SendMessage(userId, serviceResult.Error);
                return;
            }

            var result = serviceResult.Result;
            var hasKeyboard = result.MenuKeyboard is not null;
            if (hasKeyboard)
            {
                var keyboard = _keyboardBuilder.Build(result.MenuKeyboard);
                await _telegramBotService.SendKeyboard(userId, result.Message, keyboard);
                return;
            }

            await _telegramBotService.SendMessage(userId, result.Message);
        }
    }
}