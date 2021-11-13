using System.Threading.Tasks;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Telegram.Client.Abstract;
using PriceObserver.Telegram.Dialog.Input.Abstract;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Client.Concrete
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly IUpdateToUpdateDtoConverter _updateDtoConverter;
        private readonly IInputHandler _inputHandler;
        private readonly ITelegramBotService _telegramBotService;
        
        public UpdateHandler(
            IUpdateToUpdateDtoConverter updateDtoConverter,
            IInputHandler inputHandler, 
            ITelegramBotService telegramBotService)
        {
            _updateDtoConverter = updateDtoConverter;
            _inputHandler = inputHandler;
            _telegramBotService = telegramBotService;
        }

        public async Task Handle(Update update)
        {
            var updateDto = _updateDtoConverter.Convert(update);
            var userId = updateDto.UserId;

            var result = await _inputHandler.Handle(updateDto);

            if (!result.IsSuccess)
            {
                await _telegramBotService.SendMessage(userId, result.Error);
                return;
            }

            var hasKeyboard = result.Result.MenuKeyboard != null;
            if (hasKeyboard)
            {
                await _telegramBotService.SendKeyboard(userId, result.Result.Message, result.Result.MenuKeyboard);
                return;
            }

            await _telegramBotService.SendMessage(userId, result.Result.Message);
        }
    }
}