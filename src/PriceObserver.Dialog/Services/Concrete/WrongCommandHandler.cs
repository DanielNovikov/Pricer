using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Models.Abstract;
using PriceObserver.Dialog.Services.Abstract;

namespace PriceObserver.Dialog.Services.Concrete;

public class WrongCommandHandler : IWrongCommandHandler
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;

    public WrongCommandHandler(
        IUserActionLogger userActionLogger,
        IMenuKeyboardBuilder menuKeyboardBuilder)
    {
        _userActionLogger = userActionLogger;
        _menuKeyboardBuilder = menuKeyboardBuilder;
    }

    public IReplyResult Handle(MessageModel message)
    {
        _userActionLogger.LogWrongCommand(message.User, message.Text);

        var menuKey = message.User.MenuKey;
        var keyboard = _menuKeyboardBuilder.Build(menuKey);

        return new ReplyKeyboardResult(keyboard, ResourceKey.Dialog_IncorrectCommand);
    }
}