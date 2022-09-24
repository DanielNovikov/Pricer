using Pricer.Dialog.Common.Models.Input;
using Telegram.Bot.Types.ReplyMarkups;

namespace Pricer.Dialog.Telegram.Services.Abstract;

public interface IReplyKeyboardMarkupBuilder
{
    IReplyMarkup Build(MenuKeyboard keyboard);
}