using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Commands.Concrete.Handlers;

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

    public async Task<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogWebsiteCalled(user);

        var loginUrl = await _websiteLoginUrlBuilder.Build(user.Id);
            
        var result = new ReplyResourceResult(ResourceKey.Dialog_Website, loginUrl);
        return CommandHandlingServiceResult.Success(result);
    }
}