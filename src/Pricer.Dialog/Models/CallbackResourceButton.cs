using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Models;

public record CallbackResourceButton(ResourceKey Resource, string Data) : IMessageKeyboardButton;