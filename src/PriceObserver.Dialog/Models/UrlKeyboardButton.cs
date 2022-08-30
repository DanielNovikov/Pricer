using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Dialog.Models;

public record UrlKeyboardButton(ResourceKey Text, string Url) : IMessageKeyboardButton;