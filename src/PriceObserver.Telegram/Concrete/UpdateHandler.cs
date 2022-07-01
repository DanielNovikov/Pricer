using System.Threading.Tasks;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Telegram.Abstract;
using PriceObserver.Telegram.Extensions;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Concrete;

public class UpdateHandler : IUpdateHandler
{
    private readonly IInputHandler _inputHandler;
    private readonly ITelegramBotService _telegramBotService;
    private readonly IReplyKeyboardMarkupBuilder _keyboardBuilder;
    private readonly IResourceService _resourceService;
        
    public UpdateHandler(
        IInputHandler inputHandler, 
        ITelegramBotService telegramBotService,
        IReplyKeyboardMarkupBuilder keyboardBuilder, 
        IResourceService resourceService)
    {
        _inputHandler = inputHandler;
        _telegramBotService = telegramBotService;
        _keyboardBuilder = keyboardBuilder;
        _resourceService = resourceService;
    }

    public async Task Handle(Update update)
    {
        var updateDto = update.ToDto();
        var userExternalId = updateDto.UserExternalId;

        var serviceResult = await _inputHandler.Handle(updateDto);

        if (!serviceResult.IsSuccess)
        {
            var errorMessage = _resourceService.Get(serviceResult.Error);
            await _telegramBotService.SendMessage(userExternalId, errorMessage);
            return;
        }

        var result = serviceResult.Result;
        var hasKeyboard = result.MenuKeyboard is not null;
        if (hasKeyboard)
        {
            var keyboard = _keyboardBuilder.Build(result.MenuKeyboard);
            await _telegramBotService.SendMessageWithKeyboard(userExternalId, result.Message, keyboard);
            return;
        }

        await _telegramBotService.SendMessage(userExternalId, result.Message);
    }
}