﻿using PriceObserver.Common.Services.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Callbacks.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using System.Text.Json;
using System.Threading.Tasks;

namespace PriceObserver.Dialog.Callbacks.Concrete.Handlers;

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

		var message = _resourceService.Get(ResourceKey.Dialog_ItemAdded);
		
		var partnerUrl = _partnerUrlBuilder.Build(item.Url);
		var goByItemUrlButton = new UrlKeyboardButton(ResourceKey.Dialog_GoByItemUrl, partnerUrl);
		var keyboard = new MessageKeyboard(goByItemUrlButton);

		var callbackResult = new CallbackResult(message, keyboard);

		return CallbackHandlingResult.Success(callbackResult);
	}
}