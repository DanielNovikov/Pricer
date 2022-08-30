using PriceObserver.Common.Services.Abstract;
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
	private readonly IItemService _itemService;
	private readonly IItemRepository _itemRepository;
	private readonly IUserActionLogger _userActionLogger;
	private readonly ICallbackDataBuilder _callbackDataBuilder;
	private readonly IPartnerUrlBuilder _partnerUrlBuilder;

	public DeleteItemCallbackHandler(
		IResourceService resourceService,
		IItemService itemService,
		IItemRepository itemRepository,
		IUserActionLogger userActionLogger,
		ICallbackDataBuilder callbackDataBuilder,
		IPartnerUrlBuilder partnerUrlBuilder)
	{
		_resourceService = resourceService;
		_itemService = itemService;
		_itemRepository = itemRepository;
		_userActionLogger = userActionLogger;
		_callbackDataBuilder = callbackDataBuilder;
		_partnerUrlBuilder = partnerUrlBuilder;
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
		
		var callbackDataJson = _callbackDataBuilder.BuildJson(CallbackKey.RestoreItem, item.Id);
		var restoreItemButton = new CallbackKeyboardButton(ResourceKey.Dialog_RestoreItem, callbackDataJson);
		
		var partnerUrl = _partnerUrlBuilder.Build(item.Url);
		var goByItemUrlButton = new UrlKeyboardButton(ResourceKey.Dialog_GoByItemUrl, partnerUrl);

		var keyboard = new MessageKeyboard(restoreItemButton, goByItemUrlButton);
		
		var result = new CallbackResult(keyboardMessage, keyboard);
		await _itemService.Delete(item);

		return CallbackHandlingResult.Success(result);
	}
}