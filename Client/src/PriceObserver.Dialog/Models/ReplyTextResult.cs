using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Models;

public record ReplyTextResult(string Text) : IReplyResult;