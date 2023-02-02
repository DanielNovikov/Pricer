using Pricer.Bot.Telegram.Abstract;
using Pricer.Dialog.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pricer.Bot.Telegram.Concrete;

public class ReplyMarkupBuilder : IReplyMarkupBuilder
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