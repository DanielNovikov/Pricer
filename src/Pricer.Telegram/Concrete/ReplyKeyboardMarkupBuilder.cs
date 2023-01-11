using System.Linq;
using Pricer.Dialog.Models;
using Pricer.Telegram.Abstract;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pricer.Telegram.Concrete;

public class ReplyKeyboardMarkupBuilder : IReplyKeyboardMarkupBuilder
{
    public IReplyMarkup Build(MenuKeyboard keyboard)
    {
        var buttons = keyboard.Buttons
            .Select(x => x.Select(y => new KeyboardButton(y.Title)));

        return new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true
        };
    }
}