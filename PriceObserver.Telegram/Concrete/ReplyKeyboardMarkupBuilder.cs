﻿using System.Linq;
using PriceObserver.Model.Dialog.Common;
using PriceObserver.Telegram.Abstract;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Concrete
{
    public class ReplyKeyboardMarkupBuilder : IReplyKeyboardMarkupBuilder
    {
        public ReplyKeyboardMarkup Build(MenuKeyboard keyboard)
        {
            var buttons = keyboard.ButtonsGrid
                .Select(x => x.Select(y => new KeyboardButton(y.Title)));

            return new ReplyKeyboardMarkup(buttons, true);
        }
    }
}