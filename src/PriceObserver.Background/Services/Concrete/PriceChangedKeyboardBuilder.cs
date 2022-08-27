using PriceObserver.Background.Services.Abstract;
using PriceObserver.Common.Services.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Telegram.Abstract;
using System.Collections.Generic;
using System.Text.Json;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Background.Services.Concrete;

public class PriceChangedKeyboardBuilder : IPriceChangedKeyboardBuilder
{
	private readonly IInlineKeyboardMarkupBuilder _keyboardBuilder;
	private readonly IPartnerUrlBuilder _partnerUrlBuilder;
	private readonly IResourceService _resourceService;

	public PriceChangedKeyboardBuilder(
		IInlineKeyboardMarkupBuilder keyboardBuilder,
		IPartnerUrlBuilder partnerUrlBuilder,
		IResourceService resourceService)
	{
		_keyboardBuilder = keyboardBuilder;
		_partnerUrlBuilder = partnerUrlBuilder;
		_resourceService = resourceService;
	}

	public InlineKeyboardMarkup Build(Item item)
	{
		var partnerUrl = _partnerUrlBuilder.Build(item.Url);

		var callbackParameters = new List<object> { item.Id };
		var callbackData = new CallbackData(CallbackKey.DeleteItem, callbackParameters);
		var callbackDataJson = JsonSerializer.Serialize(callbackData);

		var deleteItemMessage = _resourceService.Get(ResourceKey.Background_DeleteItem);
		var goByItemUrlMessage = _resourceService.Get(ResourceKey.Background_GoByItemUrl);
		
		var keyboardButtons = new List<List<IMessageKeyboardButton>>
		{
			new()
			{
				new CallbackKeyboardButton(deleteItemMessage, callbackDataJson),
				new UrlKeyboardButton(goByItemUrlMessage, partnerUrl)
			}
		};

		var keyboard = new MessageKeyboard(keyboardButtons);

		return _keyboardBuilder.Build(keyboard);
	}
}