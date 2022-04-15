using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Concrete;

public class UserRedirectionService : IUserRedirectionService
{
    private readonly IUserService _userService;
    private readonly IReplyWithKeyboardBuilder _replyWithKeyboardBuilder;
    private readonly IUserActionLogger _userActionLogger;

    public UserRedirectionService(
        IUserService userService,
        IReplyWithKeyboardBuilder replyWithKeyboardBuilder,
        IUserActionLogger userActionLogger)
    {
        _userService = userService;
        _replyWithKeyboardBuilder = replyWithKeyboardBuilder;
        _userActionLogger = userActionLogger;
    }

    public async Task<ReplyResult> Redirect(User user, Menu menuToRedirect)
    {
        _userActionLogger.LogRedirectToMenu(user, menuToRedirect);
        
        await _userService.RedirectToMenu(user, menuToRedirect);

        return _replyWithKeyboardBuilder.Build(menuToRedirect);
    }
}