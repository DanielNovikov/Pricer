using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Dialog.Commands.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Commands.Concrete.Handlers;

public class AddCommandHandler : ICommandHandler
{
    private readonly IUserActionLogger _userActionLogger;
    private readonly ICallbackDataBuilder _callbackDataBuilder;
    private readonly IItemRepository _itemRepository;

    public AddCommandHandler(
        IUserActionLogger userActionLogger,
        ICallbackDataBuilder callbackDataBuilder, 
        IItemRepository itemRepository)
    {
        _userActionLogger = userActionLogger;
        _callbackDataBuilder = callbackDataBuilder;
        _itemRepository = itemRepository;
    }

    public CommandKey Key => CommandKey.Add;
    
    public async ValueTask<IReplyResult> Handle(User user)
    {
        _userActionLogger.LogGotAddItemInstruction(user);

        var userHasItems = await _itemRepository.Any(user.Id);

        if (userHasItems)
            return new ReplyResourceResult(ResourceKey.Dialog_AddItemInformation);
        
        var callbackData = _callbackDataBuilder.BuildJson(CallbackKey.AddExample);
            
        var button = new CallbackResourceButton(ResourceKey.Dialog_AddExample, callbackData);
        var keyboard = new MessageKeyboard(button);

        return new ReplyKeyboardResult(keyboard, ResourceKey.Dialog_AddItemInformation);
    }
}