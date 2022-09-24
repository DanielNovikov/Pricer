using PriceObserver.Dialog.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace PriceObserver.Telegram.Abstract;

public interface IReplyKeyboardMarkupBuilder
{
    IReplyMarkup Build(MenuKeyboard keyboard);
}