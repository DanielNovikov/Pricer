using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Dialog.Models;

public record CallbackData(CallbackKey Key, object[] Parameters);