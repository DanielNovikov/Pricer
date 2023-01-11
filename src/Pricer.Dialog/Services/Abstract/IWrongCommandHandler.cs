using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Services.Abstract;

public interface IWrongCommandHandler
{
    IReplyResult Handle(MessageModel message);
}