using System.Text.Json;
using System.Threading.Tasks;
using Pricer.Common.Services.Abstract;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Callbacks.Abstract;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Callbacks.Concrete.Handlers;

public class DeleteItemCallbackHandler : ICallbackHandler
{
	private readonly IItemService _itemService;
	private readonly IItemRepository _itemRepository;
	private readonly IUserActionLogger _userActionLogger;
	private readonly ICallbackDataBuilder _callbackDataBuilder;
	private readonly IPartnerUrlBuilder _partnerUrlBuilder;

	public DeleteItemCallbackHandler(
		IItemService itemService,
		IItemRepository itemRepository,
		IUserActionLogger userActionLogger,
		ICallbackDataBuilder callbackDataBuilder,
		IPartnerUrlBuilder partnerUrlBuilder)
	{
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
		await _itemService.Delete(item);
		
		var callbackDataJson = _callbackDataBuilder.BuildJson(CallbackKey.RestoreItem, item.Id);
		var restoreItemButton = new CallbackResourceButton(ResourceKey.Dialog_RestoreItem, callbackDataJson);
		
		var partnerUrl = _partnerUrlBuilder.Build(item.Url);
		var goByItemUrlButton = new UrlButton(ResourceKey.Dialog_GoByItemUrl, partnerUrl);

		var keyboard = new MessageKeyboard(restoreItemButton, goByItemUrlButton);
		
		var result = new ReplyKeyboardResult(keyboard, ResourceKey.Dialog_ItemDeleted);
		return CallbackHandlingResult.Success(result);
	}
}