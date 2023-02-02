using Pricer.Dialog.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pricer.Bot.Telegram.Abstract;

public interface IReplyMarkupBuilder
{
    IReplyMarkup Build(MenuKeyboard keyboard);
}