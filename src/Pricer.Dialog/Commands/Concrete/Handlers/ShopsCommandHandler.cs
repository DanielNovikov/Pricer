using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Commands.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Commands.Concrete.Handlers;

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
        
    public ValueTask<IReplyResult> Handle(User user)
    {
        _userActionLogger.LogShopsCalled(user);
            
        var shopsInfoMessage = _shopCategoriesMessageBuilder.Build();
        var replyResult = new ReplyTextResult(shopsInfoMessage);
        
        return ValueTask.FromResult<IReplyResult>(replyResult);
    }
}