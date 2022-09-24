using Pricer.Dialog.Common.Models.Abstract;
using Pricer.Dialog.Input.Models;

namespace Pricer.Dialog.Input.Services.Abstract;

public interface IWrongCommandHandler
{
    IReplyResult Handle(MessageModel message);
}