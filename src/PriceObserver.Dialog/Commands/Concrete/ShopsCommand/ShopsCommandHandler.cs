﻿using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;

namespace PriceObserver.Dialog.Commands.Concrete.ShopsCommand;

public class ShopsCommandHandler : ICommandHandler
{
    private readonly IShopsInfoMessageBuilder _shopsInfoMessageBuilder;
    private readonly IUserActionLogger _userActionLogger;
        
    public ShopsCommandHandler(
        IShopsInfoMessageBuilder shopsInfoMessageBuilder,
        IUserActionLogger userActionLogger)
    {
        _shopsInfoMessageBuilder = shopsInfoMessageBuilder;
        _userActionLogger = userActionLogger;
    }

    public CommandKey Type => CommandKey.Shops;
        
    public Task<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogShopsCalled(user);
            
        var shopsInfoMessage = _shopsInfoMessageBuilder.Build();

        var result = ReplyResult.Reply(shopsInfoMessage);
        var serviceResult = CommandHandlingServiceResult.Success(result);
            
        return Task.FromResult(serviceResult);
    }
}