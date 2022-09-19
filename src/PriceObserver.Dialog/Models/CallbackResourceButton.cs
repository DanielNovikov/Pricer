using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Dialog.Models;

public record CallbackResourceButton(ResourceKey Resource, string Data) : IMessageKeyboardButton;