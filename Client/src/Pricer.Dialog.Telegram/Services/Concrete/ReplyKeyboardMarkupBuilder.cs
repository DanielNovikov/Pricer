using Pricer.Dialog.Common.Models.Input;
using Pricer.Dialog.Telegram.Services.Abstract;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pricer.Dialog.Telegram.Services.Concrete;

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