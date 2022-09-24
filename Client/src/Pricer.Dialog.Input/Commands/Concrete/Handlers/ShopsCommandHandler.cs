using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Common.Models;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Input.Commands.Abstract;
using Pricer.Dialog.Input.Models;
using Pricer.Dialog.Input.Services.Abstract;

namespace Pricer.Dialog.Input.Commands.Concrete.Handlers;

public class ShopsCommandHandler : ICommandHandler
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly IShopCategoriesMessageBuilder _shopCategoriesMessageBuilder;
        
    public ShopsCommandHandler(
        IUserActionLogger userActionLogger,
        IShopCategoriesMessageBuilder shopCategoriesMessageBuilder)
    {
        _userActionLogger = userActionLogger;
        _shopCategoriesMessageBuilder = shopCategoriesMessageBuilder;
    }

    public CommandKey Key => CommandKey.Shops;
        
    public ValueTask<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogShopsCalled(user);
            
        var shopsInfoMessage = _shopCategoriesMessageBuilder.Build();

        var replyResult = new ReplyTextResult(shopsInfoMessage);
        var serviceResult = CommandHandlingServiceResult.Success(replyResult);
            
        return ValueTask.FromResult(serviceResult);
    }
}