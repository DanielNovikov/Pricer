namespace PriceObserver.Dialog.Models;

public record CallbackKeyboardButton(string Text, string Data) : IMessageKeyboardButton;