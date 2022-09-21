using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Commands.Concrete.Handlers;

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
        
    public Task<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogShopsCalled(user);
            
        var shopsInfoMessage = _shopCategoriesMessageBuilder.Build();

        var replyResult = new ReplyTextResult(shopsInfoMessage);
        var serviceResult = CommandHandlingServiceResult.Success(replyResult);
            
        return Task.FromResult(serviceResult);
    }
}