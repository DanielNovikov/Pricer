using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Concrete.WebsiteCommand.Options;
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
    private readonly WebsiteCommandOptions _options;

    public WebsiteCommandHandler(
        IUserTokenRepository userTokenRepository,
        IUserTokenService userTokenService, 
        IUserActionLogger userActionLogger, 
        IResourceService resourceService,
        IOptionsSnapshot<WebsiteCommandOptions> options)
    {
        _userTokenRepository = userTokenRepository;
        _userTokenService = userTokenService;
        _userActionLogger = userActionLogger;
        _resourceService = resourceService;
        _options = options.Value;
    }

    public CommandKey Type => CommandKey.Website;

    private const string LoginEndpoint = "{0}/login/{1}";

    public async Task<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogWebsiteCalled(user);
            
        var userToken = await _userTokenRepository.GetNotExpiredByUserId(user.Id);

        if (userToken is null)
            userToken = await _userTokenService.CreateForUser(user.Id);

        var endpoint = string.Format(LoginEndpoint, _options.Url, userToken.Token);
        var message = _resourceService.Get(ResourceKey.Dialog_Website, endpoint);
            
        var result = ReplyResult.Reply(message);
        return CommandHandlingServiceResult.Success(result);
    }
}