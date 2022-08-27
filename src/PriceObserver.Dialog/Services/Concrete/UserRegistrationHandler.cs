using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Extensions;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Services.Concrete;

public class UserRegistrationHandler : IUserRegistrationHandler
{
    private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;
    private readonly IShopsMessageBuilder _shopsMessageBuilder;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IResourceService _resourceService;
    private readonly IMenuService _menuService;
    private readonly ICommandService _commandService;
    private readonly IAuthorizationService _authorizationService;

    private const int LimitCountOfShops = 5;
    
    public UserRegistrationHandler(
        IMenuKeyboardBuilder menuKeyboardBuilder, 
        IShopsMessageBuilder shopsMessageBuilder, 
        IUserActionLogger userActionLogger, 
        IResourceService resourceService, 
        IMenuService menuService,
        ICommandService commandService,
        IAuthorizationService authorizationService)
    {
        _menuKeyboardBuilder = menuKeyboardBuilder;
        _shopsMessageBuilder = shopsMessageBuilder;
        _userActionLogger = userActionLogger;
        _resourceService = resourceService;
        _menuService = menuService;
        _commandService = commandService;
        _authorizationService = authorizationService;
    }

    public async Task<ReplyResult> Handle(UserModel userModel)
    {
        var user = await _authorizationService.Register(userModel);
        
        _userActionLogger.LogUserRegistered(user);

        var helpCommandTitle = _commandService.GetTitle(CommandKey.Help);
        var shopsInfoMessage = _shopsMessageBuilder.Build(LimitCountOfShops);

        var menuText = _menuService.GetTitle(user.MenuKey); 
                
        var message = _resourceService.Get(
            ResourceKey.Dialog_UserRegistered,
            user.GetFullName(), helpCommandTitle, shopsInfoMessage, menuText);

        var menuKeyboard = _menuKeyboardBuilder.Build(user.MenuKey);

        return ReplyResult.ReplyWithKeyboard(message, menuKeyboard);
    }
}