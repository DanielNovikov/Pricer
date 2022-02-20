using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Commands.Concrete.WebsiteCommand;

public class WebsiteCommandHandler : ICommandHandler
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly IResourceService _resourceService;
    private readonly IWebsiteLoginUrlBuilder _websiteLoginUrlBuilder;

    public WebsiteCommandHandler(
        IUserActionLogger userActionLogger, 
        IResourceService resourceService, 
        IWebsiteLoginUrlBuilder websiteLoginUrlBuilder)
    {
        _userActionLogger = userActionLogger;
        _resourceService = resourceService;
        _websiteLoginUrlBuilder = websiteLoginUrlBuilder;
    }

    public CommandKey Type => CommandKey.Website;

    public async Task<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogWebsiteCalled(user);

        var loginUrl = await _websiteLoginUrlBuilder.Build(user.Id);
        var message = _resourceService.Get(ResourceKey.Dialog_Website, loginUrl);
            
        var result = ReplyResult.Reply(message);
        return CommandHandlingServiceResult.Success(result);
    }
}