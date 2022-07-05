using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;

namespace PriceObserver.Dialog.Services.Concrete;

public class UserRedirectionService : IUserRedirectionService
{
    private readonly IUserService _userService;
    private readonly IReplyWithKeyboardBuilder _replyWithKeyboardBuilder;

    public UserRedirectionService(
        IUserService userService,
        IReplyWithKeyboardBuilder replyWithKeyboardBuilder)
    {
        _userService = userService;
        _replyWithKeyboardBuilder = replyWithKeyboardBuilder;
    }

    public async Task<ReplyResult> Redirect(User user, Menu menuToRedirect)
    {
        await _userService.RedirectToMenu(user, menuToRedirect);

        return _replyWithKeyboardBuilder.Build(menuToRedirect);
    }
}