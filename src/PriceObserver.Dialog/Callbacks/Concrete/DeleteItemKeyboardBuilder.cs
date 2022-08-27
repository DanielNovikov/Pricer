using PriceObserver.Common.Services.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Callbacks.Abstract;
using PriceObserver.Dialog.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace PriceObserver.Dialog.Callbacks.Concrete;

public class DeleteItemKeyboardBuilder : IDeleteItemKeyboardBuilder
{
	private readonly IResourceService _resourceService;
	private readonly IPartnerUrlBuilder _partnerUrlBuilder;

	public DeleteItemKeyboardBuilder(
		IResourceService resourceService,
		IPartnerUrlBuilder partnerUrlBuilder)
	{
		_resourceService = resourceService;
		_partnerUrlBuilder = partnerUrlBuilder;
	}

	public MessageKeyboard Build(Item item)
	{
		var partnerUrl = _partnerUrlBuilder.Build(item.Url);
		
		var restoreItemMessage = _resourceService.Get(ResourceKey.Dialog_RestoreItem);
		var goByItemUrlMessage = _resourceService.Get(ResourceKey.Dialog_GoByItemUrl);

		var callbackParameters = new List<object> { item.Id };
		var callbackData = new CallbackData(CallbackKey.RestoreItem, callbackParameters);
		var callbackDataJson = JsonSerializer.Serialize(callbackData);
		
		var keyboardButtons = new List<List<IMessageKeyboardButton>>
		{
			new()
			{
				new CallbackKeyboardButton(restoreItemMessage, callbackDataJson),
				new UrlKeyboardButton(goByItemUrlMessage, partnerUrl)
			}
		};

		return new MessageKeyboard(keyboardButtons);
	}
}