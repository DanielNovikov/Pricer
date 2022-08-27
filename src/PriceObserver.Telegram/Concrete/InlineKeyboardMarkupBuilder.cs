using PriceObserver.Dialog.Models;
using PriceObserver.Telegram.Abstract;
using System;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Concrete;

public class InlineKeyboardMarkupBuilder : IInlineKeyboardMarkupBuilder
{
	public InlineKeyboardMarkup Build(MessageKeyboard keyboard)
	{
		var buttons = keyboard.Buttons
			.Select(x => x.Select(BuildButton));

		return new InlineKeyboardMarkup(buttons);
	}
	
	private InlineKeyboardButton BuildButton(IMessageKeyboardButton button)
	{
		return button switch
		{
			CallbackKeyboardButton callbackButton =>
				InlineKeyboardButton.WithCallbackData(callbackButton.Text, callbackButton.Data),
			UrlKeyboardButton urlButton => 
				InlineKeyboardButton.WithUrl(urlButton.Text, urlButton.Url),
			_ => throw new InvalidOperationException($"Invalid button type: {button.GetType().FullName}")
		};
	}
}