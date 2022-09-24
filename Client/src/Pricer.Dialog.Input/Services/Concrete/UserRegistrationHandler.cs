using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Common.Extensions;
using Pricer.Dialog.Common.Models;
using Pricer.Dialog.Common.Models.Abstract;
using Pricer.Dialog.Common.Models.Callback;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Input.Services.Abstract;

namespace Pricer.Dialog.Input.Services.Concrete;

public class UserRegistrationHandler : IUserRegistrationHandler
{
    private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;
    private readonly IShopsMessageBuilder _shopsMessageBuilder;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IMenuService _menuService;
    private readonly ICommandService _commandService;
    private readonly IAuthorizationService _authorizationService;

    private const int LimitCountOfShops = 5;
    
    public UserRegistrationHandler(
        IMenuKeyboardBuilder menuKeyboardBuilder, 
        IShopsMessageBuilder shopsMessageBuilder, 
        IUserActionLogger userActionLogger, 
        IMenuService menuService,
        ICommandService commandService,
        IAuthorizationService authorizationService)
    {
        _menuKeyboardBuilder = menuKeyboardBuilder;
        _shopsMessageBuilder = shopsMessageBuilder;
        _userActionLogger = userActionLogger;
        _menuService = menuService;
        _commandService = commandService;
        _authorizationService = authorizationService;
    }

    public async Task<IReplyResult> Handle(UserModel userModel)
    {
        var user = await _authorizationService.Register(userModel);
        
        _userActionLogger.LogUserRegistered(user);

        var helpCommandTitle = _commandService.GetTitle(CommandKey.Help);
        var shopsInfoMessage = _shopsMessageBuilder.Build(LimitCountOfShops);

        var menuText = _menuService.GetTitle(user.MenuKey); 
              
        var menuKeyboard = _menuKeyboardBuilder.Build(user.MenuKey);

        return new ReplyKeyboardResult(
            menuKeyboard, 
            ResourceKey.Dialog_UserRegistered,
            user.GetFullName(), helpCommandTitle, shopsInfoMessage, menuText);
    }
}