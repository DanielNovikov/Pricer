using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Dialog.Models;

public record CallbackData(CallbackKey Key, object[] Parameters);