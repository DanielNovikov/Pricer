using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Models;

public record ReplyKeyboardResult(IReplyKeyboard Keyboard, ResourceKey Resource, params object[] Parameters) 
    : IReplyResult;