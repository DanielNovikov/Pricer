using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;

namespace PriceObserver.Dialog.Services.Concrete;

public class WrongCommandHandler : IWrongCommandHandler
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;
    private readonly IResourceService _resourceService;

    public WrongCommandHandler(
        IUserActionLogger userActionLogger,
        IMenuKeyboardBuilder menuKeyboardBuilder,
        IResourceService resourceService)
    {
        _userActionLogger = userActionLogger;
        _menuKeyboardBuilder = menuKeyboardBuilder;
        _resourceService = resourceService;
    }

    public ReplyResult Handle(MessageModel message)
    {
        _userActionLogger.LogWrongCommand(message.User, message.Text);

        var menuKey = message.User.MenuKey;
        var keyboard = _menuKeyboardBuilder.Build(menuKey);
        var replyMessage = _resourceService.Get(ResourceKey.Dialog_IncorrectCommand);

        return ReplyResult.ReplyWithKeyboard(replyMessage, keyboard);
    }
}