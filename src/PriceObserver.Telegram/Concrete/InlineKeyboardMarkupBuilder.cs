﻿using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Models;
using PriceObserver.Telegram.Abstract;
using System;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Concrete;

public class InlineKeyboardMarkupBuilder : IInlineKeyboardMarkupBuilder
{
	private readonly IResourceService _resourceService;

	public InlineKeyboardMarkupBuilder(IResourceService resourceService)
	{
		_resourceService = resourceService;
	}

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
			CallbackResourceButton callbackResourceButton =>
				InlineKeyboardButton.WithCallbackData(_resourceService.Get(callbackResourceButton.Resource), callbackResourceButton.Data),
			CallbackTextButton callbackTextButton =>
				InlineKeyboardButton.WithCallbackData(callbackTextButton.Text, callbackTextButton.Data),
			UrlButton urlButton => 
				InlineKeyboardButton.WithUrl(_resourceService.Get(urlButton.Resource), urlButton.Url),
			_ => throw new InvalidOperationException($"Invalid button type: {button.GetType().FullName}")
		};
	}
}