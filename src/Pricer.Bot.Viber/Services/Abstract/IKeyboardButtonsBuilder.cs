using Pricer.Dialog.Models;
using Pricer.Viber.Models.Message;

namespace Pricer.Viber.Services.Abstract;

public interface IKeyboardButtonsBuilder
{
    KeyboardButton[] Build(MenuKeyboard keyboard);
}