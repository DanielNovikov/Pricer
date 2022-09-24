using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Models;

public record UrlButton(ResourceKey Resource, string Url) : IMessageKeyboardButton;