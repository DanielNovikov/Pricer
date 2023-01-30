using Pricer.Dialog.Models;
using Pricer.Viber.Models.Message;

namespace Pricer.Viber.Services.Abstract;

public interface IRichMediaBuilder
{
    RichMedia Build(string text, MessageKeyboard keyboard);
}