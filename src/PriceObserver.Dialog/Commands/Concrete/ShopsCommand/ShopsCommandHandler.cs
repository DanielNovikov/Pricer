using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;

namespace PriceObserver.Dialog.Commands.Concrete.ShopsCommand;

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

    public CommandKey Type => CommandKey.Shops;
        
    public Task<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogShopsCalled(user);
            
        var shopsInfoMessage = _shopCategoriesMessageBuilder.Build();

        var result = ReplyResult.Reply(shopsInfoMessage);
        var serviceResult = CommandHandlingServiceResult.Success(result);
            
        return Task.FromResult(serviceResult);
    }
}