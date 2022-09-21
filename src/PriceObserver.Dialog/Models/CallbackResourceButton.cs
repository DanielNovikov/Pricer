using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Models;

public record CallbackResourceButton(ResourceKey Resource, string Data) : IMessageKeyboardButton;