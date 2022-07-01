﻿using PriceObserver.Dialog.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Abstract;

public interface IReplyKeyboardMarkupBuilder
{
    ReplyKeyboardMarkup Build(MenuKeyboard keyboard);
}