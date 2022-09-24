using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Models;

public record CallbackTextButton(string Text, string Data) : IMessageKeyboardButton;