using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Common.Models.Abstract;
using Pricer.Dialog.Common.Models.Callback;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Input.Commands.Abstract;
using Pricer.Dialog.Input.Models;

namespace Pricer.Dialog.Input.Commands.Concrete.Handlers;

public class ChangeMinimumDiscountCommandHandler : ICommandHandler
{
    private readonly int[] _discountThresholds =
    {
        5,
        15,
        30,
        50
    };
    
    private readonly ICallbackDataBuilder _callbackDataBuilder;
    private readonly IUserActionLogger _userActionLogger;
    
    public CommandKey Key => CommandKey.ChangeMinimumDiscount;

    public ChangeMinimumDiscountCommandHandler(
        ICallbackDataBuilder callbackDataBuilder, 
        IUserActionLogger userActionLogger)
    {
        _callbackDataBuilder = callbackDataBuilder;
        _userActionLogger = userActionLogger;
    }

    public ValueTask<CommandHandlingServiceResult> Handle(User user)
    {
        _userActionLogger.LogCalledChangingMinimumDiscountThreshold(user);
        
        var buttons = _discountThresholds
            .Where(x => x != user.MinimumDiscountThreshold)
            .Select(x =>
            {
                var data = _callbackDataBuilder.BuildJson(CallbackKey.ChangeMinimumDiscountThreshold, x);
                return new CallbackTextButton(x.ToString() + '%', data);
            })
            .Cast<IMessageKeyboardButton>()
            .ToList();

        var keyboard = new MessageKeyboard(buttons);
        
        var result = new ReplyKeyboardResult(
            keyboard, 
            ResourceKey.Dialog_ChangeMinimumDiscountThreshold, 
            user.MinimumDiscountThreshold);
        
        return ValueTask.FromResult(CommandHandlingServiceResult.Success(result));
    }
}