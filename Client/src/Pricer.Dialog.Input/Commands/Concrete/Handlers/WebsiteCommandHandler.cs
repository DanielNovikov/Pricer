﻿using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Common.Models.Callback;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Input.Commands.Abstract;
using Pricer.Dialog.Input.Models;
using Pricer.Dialog.Input.Services.Abstract;

namespace Pricer.Dialog.Input.Commands.Concrete.Handlers;

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

    public async ValueTask<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogWebsiteCalled(user);

        var loginUrl = await _websiteLoginUrlBuilder.Build(user.Id);
       
#if DEBUG
        var result = new ReplyResourceResult(ResourceKey.Dialog_Website, loginUrl);
#else
        var goToWebsiteButton = new UrlButton(ResourceKey.Dialog_GoToWebsite, loginUrl);
        var keyboard = new MessageKeyboard(goToWebsiteButton);
        var result = new ReplyKeyboardResult(keyboard, ResourceKey.Dialog_Website);
#endif  
   
        return CommandHandlingServiceResult.Success(result);
    }
}