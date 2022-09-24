using Pricer.Data.InMemory.Models;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Common.Models.Abstract;
using Pricer.Dialog.Common.Models.Callback;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Input.Services.Abstract;

namespace Pricer.Dialog.Input.Services.Concrete;

public class UserRedirectionService : IUserRedirectionService
{
    private readonly IUserService _userService;
    private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;

    public UserRedirectionService(
        IUserService userService, 
        IMenuKeyboardBuilder menuKeyboardBuilder)
    {
        _userService = userService;
        _menuKeyboardBuilder = menuKeyboardBuilder;
    }

    public async Task<IReplyResult> Redirect(User user, Menu menuToRedirect)
    {
        await _userService.RedirectToMenu(user, menuToRedirect);

        var keyboard = _menuKeyboardBuilder.Build(menuToRedirect.Key);
        return new ReplyKeyboardResult(keyboard, menuToRedirect.Title);
    }
}