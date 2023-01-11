using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using Pricer.Dialog.Models;

namespace Pricer.Dialog.Commands.Abstract;

public interface ICommandHandlerService
{
    Task<CommandHandlingServiceResult> Handle(Command command, MessageModel message);
}