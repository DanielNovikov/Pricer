using System.Threading.Tasks;
using PriceObserver.Common.Extensions;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Common.Models;
using PriceObserver.Dialog.Input.Abstract;
using PriceObserver.Dialog.Menus.Abstract;

namespace PriceObserver.Dialog.Input.Concrete;

public class UserRegistrationHandler : IUserRegistrationHandler
{
    private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;
    private readonly IShopsInfoMessageBuilder _shopsInfoMessageBuilder;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IResourceService _resourceService;
    private readonly IMenuService _menuService;
    private readonly ICommandService _commandService;
        
    public UserRegistrationHandler(
        IMenuKeyboardBuilder menuKeyboardBuilder, 
        IShopsInfoMessageBuilder shopsInfoMessageBuilder, 
        IUserActionLogger userActionLogger, 
        IResourceService resourceService, 
        IMenuService menuService,
        ICommandService commandService)
    {
        _menuKeyboardBuilder = menuKeyboardBuilder;
        _shopsInfoMessageBuilder = shopsInfoMessageBuilder;
        _userActionLogger = userActionLogger;
        _resourceService = resourceService;
        _menuService = menuService;
        _commandService = commandService;
    }

    public async Task<ReplyResult> Handle(User user)
    {
        _userActionLogger.LogUserRegistered(user);

        var helpCommandTitle = _commandService.GetTitle(CommandKey.Help);
        var shopsInfoMessage = _shopsInfoMessageBuilder.Build();

        var menuText = _menuService.GetTitle(user.MenuKey); 
                
        var message = _resourceService.Get(
            ResourceKey.Dialog_UserRegistered,
            user.GetFullName(),
            helpCommandTitle,
            shopsInfoMessage,
            menuText);

        var menuKeyboard = await _menuKeyboardBuilder.Build(user.MenuKey);

        return ReplyResult.ReplyWithKeyboard(message, menuKeyboard);
    }
}