using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Dialog.Common.Models.Callback;

public record CallbackData(CallbackKey Key, object[] Parameters);