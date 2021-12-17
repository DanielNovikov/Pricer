using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Input.Models;

namespace PriceObserver.Dialog.Commands.Abstract
{
    public interface ICommandHandlerService
    {
        Task<CommandHandlingServiceResult> Handle(Command command, MessageDto message);
    }
}