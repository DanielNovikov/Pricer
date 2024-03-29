﻿using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Commands.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Commands.Concrete.Handlers;

public class HelpCommandHandler : ICommandHandler
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly ICommandService _commandService;
        
    public HelpCommandHandler(
        IUserActionLogger userActionLogger,
        ICommandService commandService)
    {
        _userActionLogger = userActionLogger;
        _commandService = commandService;
    }

    public CommandKey Key => CommandKey.Help;
        
    public ValueTask<IReplyResult> Handle(User user)
    {
        _userActionLogger.LogHelpCalled(user);

        var addCommandTitle = _commandService.GetTitle(CommandKey.Add);
        var allItemsCommandTitle = _commandService.GetTitle(CommandKey.AllItems);
        var shopsCommandTitle = _commandService.GetTitle(CommandKey.Shops);
        var websiteCommandTitle = _commandService.GetTitle(CommandKey.Website);
        var writeToSupportCommandTitle = _commandService.GetTitle(CommandKey.WriteToSupport);

        var result = new ReplyResourceResult(
            ResourceKey.Dialog_Help,
            addCommandTitle, allItemsCommandTitle, shopsCommandTitle, websiteCommandTitle, writeToSupportCommandTitle);
        
        return ValueTask.FromResult<IReplyResult>(result);
    }
}