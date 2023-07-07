using System.Threading.Tasks;
using Pricer.Common.Services.Abstract;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Callbacks.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;
using Pricer.Parser.Abstract;

namespace Pricer.Dialog.Callbacks.Concrete.Handlers;

public class AddExampleCallbackHandler : ICallbackHandler
{
    private readonly IExampleItemService _exampleItemService;
    private readonly IUserActionLogger _userActionLogger;
    private readonly IItemRepository _itemRepository;
    private readonly IItemService _itemService;
    private readonly IParser _parser;
    private readonly IPartnerUrlBuilder _partnerUrlBuilder;

    public AddExampleCallbackHandler(
        IExampleItemService exampleItemService, 
        IUserActionLogger userActionLogger,
        IItemRepository itemRepository,
        IItemService itemService,
        IParser parser, 
        IPartnerUrlBuilder partnerUrlBuilder)
    {
        _exampleItemService = exampleItemService;
        _userActionLogger = userActionLogger;
        _itemRepository = itemRepository;
        _itemService = itemService;
        _parser = parser;
        _partnerUrlBuilder = partnerUrlBuilder;
    }

    public CallbackKey Key => CallbackKey.AddExample;
    
    public async Task<CallbackHandlingResult> Handle(CallbackModel callback)
    {
        var user = callback.User;
        _userActionLogger.LogExampleClicked(user);
        
        var exampleResult = await _exampleItemService.Get();
        if (!exampleResult.IsSuccess)
            return CallbackHandlingResult.Success(new ReplyResourceResult(ResourceKey.Dialog_ErrorOccured));

        var url = exampleResult.Result;
        var item = await _itemRepository.GetByUserIdAndUrl(user.Id, url);
        if (item is not null)
        {
            _userActionLogger.LogDuplicateItem(user, url);
            return CallbackHandlingResult.Success(new ReplyResourceResult(ResourceKey.Dialog_DuplicateItem));
        }
        
        var parseResult = await _parser.Parse(url, _exampleItemService.ShopKey);
        if (!parseResult.IsSuccess)
        {
            _userActionLogger.LogParsingError(user, url, parseResult.Error);
            return CallbackHandlingResult.Success(new ReplyResourceResult(parseResult.Error));
        }

        var parsedItem = parseResult.Result;

        item = await _itemService.Create(
            parsedItem.Price, parsedItem.Title, url, parsedItem.ImageUrl,
            user.Id, _exampleItemService.ShopKey, parsedItem.IsAvailable, parsedItem.CurrencyKey);
        
        if (item.IsAvailable)
            _userActionLogger.LogItemAdded(user, item);
        else 
            _userActionLogger.LogNotAvailableItemAdded(user, item);

        return CallbackHandlingResult.Success(new ReplyResourceResult(ResourceKey.Dialog_ExampleAdded));

    }
}