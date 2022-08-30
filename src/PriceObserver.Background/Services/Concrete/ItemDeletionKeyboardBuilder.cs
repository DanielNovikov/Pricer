using PriceObserver.Background.Services.Abstract;
using PriceObserver.Common.Services.Abstract;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Services.Abstract;
using PriceObserver.Telegram.Abstract;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Background.Services.Concrete;

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
		var deleteItemButton = new CallbackKeyboardButton(ResourceKey.Background_DeleteItem, callbackDataJson);
		
		var partnerUrl = _partnerUrlBuilder.Build(item.Url);
		var goByItemUrlButton = new UrlKeyboardButton(ResourceKey.Background_GoByItemUrl, partnerUrl);

		var keyboard = new MessageKeyboard(deleteItemButton, goByItemUrlButton);

		return _keyboardBuilder.Build(keyboard);
	}
}