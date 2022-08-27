using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IWrongCommandHandler
{
    ReplyResult Handle(MessageModel message);
}