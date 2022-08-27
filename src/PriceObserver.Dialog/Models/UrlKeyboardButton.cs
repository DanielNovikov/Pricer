namespace PriceObserver.Dialog.Models;

public record UrlKeyboardButton(string Text, string Url) : IMessageKeyboardButton;