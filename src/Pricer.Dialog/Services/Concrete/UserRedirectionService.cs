using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Services.Concrete;

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