using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Dialog.Models;

public record UrlButton(ResourceKey Resource, string Url) : IMessageKeyboardButton;