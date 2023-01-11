using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Models;

public record UrlButton(ResourceKey Resource, string Url) : IMessageKeyboardButton;