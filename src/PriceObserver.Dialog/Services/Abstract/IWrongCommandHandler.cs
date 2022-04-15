using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IWrongCommandHandler
{
    ReplyResult Handle(MessageServiceModel message);
}