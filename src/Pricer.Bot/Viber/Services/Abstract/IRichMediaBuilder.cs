using Pricer.Bot.Viber.Models.Message;
using Pricer.Dialog.Models;

namespace Pricer.Bot.Viber.Services.Abstract;

public interface IRichMediaBuilder
{
    RichMedia Build(string text, MessageKeyboard keyboard);
}