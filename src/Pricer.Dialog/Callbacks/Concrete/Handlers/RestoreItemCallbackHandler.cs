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

public class RestoreItemCallbackHandler : ICallbackHandler
{
	private readonly IResourceService _resourceService;
	private readonly IPartnerUrlBuilder _partnerUrlBuilder;
	private readonly IItemRepository _itemRepository;
	private readonly IItemService _itemService;
	private readonly IUserActionLogger _userActionLogger;

	public RestoreItemCallbackHandler(
		IResourceService resourceService,
		IPartnerUrlBuilder partnerUrlBuilder,
		IItemRepository itemRepository,
		IItemService itemService,
		IUserActionLogger userActionLogger)
	{
		_resourceService = resourceService;
		_partnerUrlBuilder = partnerUrlBuilder;
		_itemRepository = itemRepository;
		_itemService = itemService;
		_userActionLogger = userActionLogger;
	}

	public CallbackKey Key => CallbackKey.RestoreItem;

	public async Task<CallbackHandlingResult> Handle(CallbackModel callback)
	{
		var itemIdElement = (JsonElement)callback.Parameters[0];
		var itemId = itemIdElement.GetInt32();

		var item = await _itemRepository.GetById(itemId);
		if (item is null)
			return CallbackHandlingResult.Fail();

		_userActionLogger.LogRestoredItem(callback.User, item);
		await _itemService.Restore(item);
		
		var partnerUrl = _partnerUrlBuilder.Build(item.Url);
		var goByItemUrlButton = new UrlButton(ResourceKey.Dialog_GoByItemUrl, partnerUrl);
		var keyboard = new MessageKeyboard(goByItemUrlButton);

		var callbackResult = new ReplyKeyboardResult(keyboard, ResourceKey.Dialog_ItemAdded);

		return CallbackHandlingResult.Success(callbackResult);
	}
}