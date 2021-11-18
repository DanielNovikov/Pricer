using System.Threading.Tasks;
using PriceObserver.Model.Data;
using PriceObserver.Model.Dialog.Commands;
using PriceObserver.Model.Dialog.Input;

namespace PriceObserver.Dialog.Commands.Abstract
{
    public interface ICommandHandlerService
    {
        Task<CommandHandlingServiceResult> Handle(Command command, MessageDto message);
    }
}