using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Commands.Abstract;

public interface ICommandHandlerService
{
    Task<CommandHandlingServiceResult> Handle(Command command, MessageModel message);
}