using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Models;

public record CallbackTextButton(string Text, string Data) : IMessageKeyboardButton;