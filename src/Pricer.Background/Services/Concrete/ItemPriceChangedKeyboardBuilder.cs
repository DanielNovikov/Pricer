using Pricer.Background.Services.Abstract;
using Pricer.Common.Services.Abstract;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Models;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Background.Services.Concrete;

public class ItemPriceChangedKeyboardBuilder : IItemPriceChangedKeyboardBuilder
{
	private readonly IPartnerUrlBuilder _partnerUrlBuilder;
	private readonly ICallbackDataBuilder _callbackDataBuilder;

	public ItemPriceChangedKeyboardBuilder(
		IPartnerUrlBuilder partnerUrlBuilder,
		ICallbackDataBuilder callbackDataBuilder)
	{
		_partnerUrlBuilder = partnerUrlBuilder;
		_callbackDataBuilder = callbackDataBuilder;
	}

	public MessageKeyboard Build(Item item)
	{
		var callbackDataJson = _callbackDataBuilder.BuildJson(CallbackKey.DeleteItem, item.Id);
		var deleteItemButton = new CallbackResourceButton(ResourceKey.Background_DeleteItem, callbackDataJson);
		
		var partnerUrl = _partnerUrlBuilder.Build(item.Url);
		var goByItemUrlButton = new UrlButton(ResourceKey.Background_GoByItemUrl, partnerUrl);

		return new MessageKeyboard(deleteItemButton, goByItemUrlButton);
	}
}