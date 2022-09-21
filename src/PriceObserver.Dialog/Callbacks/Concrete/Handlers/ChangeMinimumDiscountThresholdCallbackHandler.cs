using System.Text.Json;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Callbacks.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;

namespace PriceObserver.Dialog.Callbacks.Concrete.Handlers;

public class ChangeMinimumDiscountThresholdCallbackHandler : ICallbackHandler
{
    private readonly IUserService _userService;
    private readonly IUserActionLogger _userActionLogger;

    public ChangeMinimumDiscountThresholdCallbackHandler(
        IUserService userService,
        IUserActionLogger userActionLogger)
    {
        _userService = userService;
        _userActionLogger = userActionLogger;
    }

    public CallbackKey Key => CallbackKey.ChangeMinimumDiscountThreshold;
    
    public async Task<CallbackHandlingResult> Handle(CallbackModel callback)
    {
        var discountThresholdElement = (JsonElement)callback.Parameters[0];
        var discountThreshold = discountThresholdElement.GetInt32();

        var user = callback.User;

        await _userService.ChangeMinimumDiscountThreshold(user, discountThreshold);
        _userActionLogger.LogChangedMinimumDiscountThreshold(user, discountThreshold); 
        
        var replyResult = new ReplyResourceResult(ResourceKey.Dialog_MinimumDiscountThresholdChanged, discountThreshold);
        return CallbackHandlingResult.Success(replyResult);
    }
}