﻿using Pricer.Background.Services.Abstract;
using Pricer.Common.Services.Abstract;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Common.Models.Callback;
using Pricer.Dialog.Common.Services.Abstract;
using Pricer.Dialog.Telegram.Services.Abstract;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pricer.Background.Services.Concrete;

public class ItemDeletionKeyboardBuilder : IItemDeletionKeyboardBuilder
{
	private readonly IInlineKeyboardMarkupBuilder _keyboardBuilder;
	private readonly IPartnerUrlBuilder _partnerUrlBuilder;
	private readonly ICallbackDataBuilder _callbackDataBuilder;

	public ItemDeletionKeyboardBuilder(
		IInlineKeyboardMarkupBuilder keyboardBuilder,
		IPartnerUrlBuilder partnerUrlBuilder,
		ICallbackDataBuilder callbackDataBuilder)
	{
		_keyboardBuilder = keyboardBuilder;
		_partnerUrlBuilder = partnerUrlBuilder;
		_callbackDataBuilder = callbackDataBuilder;
	}

	public InlineKeyboardMarkup Build(Item item)
	{
		var callbackDataJson = _callbackDataBuilder.BuildJson(CallbackKey.DeleteItem, item.Id);
		var deleteItemButton = new CallbackResourceButton(ResourceKey.Background_DeleteItem, callbackDataJson);
		
		var partnerUrl = _partnerUrlBuilder.Build(item.Url);
		var goByItemUrlButton = new UrlButton(ResourceKey.Background_GoByItemUrl, partnerUrl);

		var keyboard = new MessageKeyboard(deleteItemButton, goByItemUrlButton);

		return _keyboardBuilder.Build(keyboard);
	}
}