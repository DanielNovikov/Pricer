using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;

namespace PriceObserver.Dialog.Commands.Concrete.Handlers;

public class ChangeMinimumDiscountCommandHandler : ICommandHandler
{
    private readonly int[] _discountThresholds =
    {
        5,
        15,
        30,
        50
    };
    
    private readonly IResourceService _resourceService;
    private readonly ICallbackDataBuilder _callbackDataBuilder;
    private readonly IUserActionLogger _userActionLogger;
    
    public CommandKey Key => CommandKey.ChangeMinimumDiscount;

    public ChangeMinimumDiscountCommandHandler(
        IResourceService resourceService,
        ICallbackDataBuilder callbackDataBuilder, 
        IUserActionLogger userActionLogger)
    {
        _resourceService = resourceService;
        _callbackDataBuilder = callbackDataBuilder;
        _userActionLogger = userActionLogger;
    }

    public Task<CommandHandlingServiceResult> Handle(User user)
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
        
        var message = _resourceService.Get(
            ResourceKey.Dialog_ChangeMinimumDiscountThreshold,
            user.MinimumDiscountThreshold);

        var result = ReplyResult.ReplyWithMessageKeyboard(message, keyboard);
        return Task.FromResult(CommandHandlingServiceResult.Success(result));
    }
}