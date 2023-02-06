using System.Threading.Tasks;
using Pricer.Data.InMemory.Models;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Commands.Abstract;

public interface ICommandHandlerService
{
    Task<IReplyResult> Handle(Command command, MessageModel message);
}