using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Callbacks.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Text.Json;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Callbacks.Concrete.Handlers;

public class DeleteItemCallbackHandler : ICallbackHandler
{
	private readonly IResourceService _resourceService;
	private readonly IDeleteItemKeyboardBuilder _keyboardBuilder;
	private readonly IItemService _itemService;
	private readonly IItemRepository _itemRepository;
	private readonly IUserActionLogger _userActionLogger;

	public DeleteItemCallbackHandler(
		IResourceService resourceService,
		IDeleteItemKeyboardBuilder keyboardBuilder,
		IItemService itemService,
		IItemRepository itemRepository,
		IUserActionLogger userActionLogger)
	{
		_resourceService = resourceService;
		_keyboardBuilder = keyboardBuilder;
		_itemService = itemService;
		_itemRepository = itemRepository;
		_userActionLogger = userActionLogger;
	}

	public CallbackKey Key => CallbackKey.DeleteItem;

	public async Task<CallbackHandlingResult> Handle(CallbackModel callback)
	{
		var itemIdElement = (JsonElement)callback.Parameters[0];
		var itemId = itemIdElement.GetInt32();
		
		var item = await _itemRepository.GetById(itemId);

		if (item is null)
			return CallbackHandlingResult.Fail();

		_userActionLogger.LogDeletedItem(callback.User, item);
		
		var keyboardMessage = _resourceService.Get(ResourceKey.Dialog_ItemDeleted);
		var keyboard = _keyboardBuilder.Build(item);
		
		var result = new CallbackResult(keyboardMessage, keyboard);
		await _itemService.Delete(item);

		return CallbackHandlingResult.Success(result);
	}
}