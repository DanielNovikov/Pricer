using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Dialog.Models;

public record CallbackKeyboardButton(ResourceKey Text, string Data) : IMessageKeyboardButton;