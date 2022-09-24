using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Common.Models.Abstract;

namespace Pricer.Dialog.Common.Models.Callback;

public record UrlButton(ResourceKey Resource, string Url) : IMessageKeyboardButton;