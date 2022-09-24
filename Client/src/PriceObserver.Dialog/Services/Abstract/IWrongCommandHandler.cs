using PriceObserver.Dialog.Models;
using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IWrongCommandHandler
{
    IReplyResult Handle(MessageModel message);
}