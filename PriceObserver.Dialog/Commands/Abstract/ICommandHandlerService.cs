using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Commands.Abstract;

public interface ICommandHandlerService
{
    Task<CommandHandlingServiceResult> Handle(Command command, MessageServiceModel message);
}