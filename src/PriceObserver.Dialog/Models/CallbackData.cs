using PriceObserver.Data.InMemory.Models.Enums;
using System.Collections.Generic;

namespace PriceObserver.Dialog.Models;

public record CallbackData(CallbackKey Key, List<object> Parameters);