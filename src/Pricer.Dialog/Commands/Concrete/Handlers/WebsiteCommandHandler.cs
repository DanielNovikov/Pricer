using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Commands.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Commands.Concrete.Handlers;

public class WebsiteCommandHandler : ICommandHandler
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly IWebsiteLoginUrlBuilder _websiteLoginUrlBuilder;

    public WebsiteCommandHandler(
        IUserActionLogger userActionLogger, 
        IWebsiteLoginUrlBuilder websiteLoginUrlBuilder)
    {
        _userActionLogger = userActionLogger;
        _websiteLoginUrlBuilder = websiteLoginUrlBuilder;
    }

    public CommandKey Key => CommandKey.Website;

    public async ValueTask<IReplyResult> Handle(User user)
    {
        _userActionLogger.LogWebsiteCalled(user);

        var loginUrl = await _websiteLoginUrlBuilder.Build(user.Id);
       
#if DEBUG
        return new ReplyResourceResult(ResourceKey.Dialog_Website, loginUrl);
#else
        var goToWebsiteButton = new UrlButton(ResourceKey.Dialog_GoToWebsite, loginUrl);
        var keyboard = new MessageKeyboard(goToWebsiteButton);
        return new ReplyKeyboardResult(keyboard, ResourceKey.Dialog_Website);
#endif
    }
}