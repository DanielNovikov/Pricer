using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Common.Models;

namespace PriceObserver.Dialog.Commands.Concrete.WebsiteCommand;

public class WebsiteCommandHandler : ICommandHandler
{
    private readonly IUserTokenRepository _userTokenRepository;
    private readonly IUserTokenService _userTokenService;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IResourceService _resourceService;
        
    public WebsiteCommandHandler(
        IUserTokenRepository userTokenRepository,
        IUserTokenService userTokenService, 
        IUserActionLogger userActionLogger, 
        IResourceService resourceService)
    {
        _userTokenRepository = userTokenRepository;
        _userTokenService = userTokenService;
        _userActionLogger = userActionLogger;
        _resourceService = resourceService;
    }

    public CommandKey Type => CommandKey.Website;
        
    public async Task<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogWebsiteCalled(user);
            
        var userToken = await _userTokenRepository.GetNotExpiredByUserId(user.Id);

        if (userToken is null)
            userToken = await _userTokenService.CreateForUser(user.Id);

        var url = $"https://pricer.ink/login/{userToken.Token}";
        var message = _resourceService.Get(ResourceKey.Dialog_Website, url);
            
        var result = ReplyResult.Reply(message);
        return CommandHandlingServiceResult.Success(result);
    }
}