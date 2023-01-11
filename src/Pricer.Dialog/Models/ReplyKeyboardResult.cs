using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Models;

public record ReplyKeyboardResult(IReplyKeyboard Keyboard, ResourceKey Resource, params object[] Parameters) 
    : IReplyResult;