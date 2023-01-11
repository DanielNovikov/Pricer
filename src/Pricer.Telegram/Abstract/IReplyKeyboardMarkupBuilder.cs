using Pricer.Dialog.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pricer.Telegram.Abstract;

public interface IReplyKeyboardMarkupBuilder
{
    IReplyMarkup Build(MenuKeyboard keyboard);
}