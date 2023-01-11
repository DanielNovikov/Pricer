using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Models;

public record ReplyResourceResult(ResourceKey Resource, params object[] Parameters) : IReplyResult;