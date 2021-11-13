using System.Threading.Tasks;
using PriceObserver.Model.Data;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Model.Telegram.Input;

namespace PriceObserver.Telegram.Dialog.Commands.Abstract
{
    public interface ICommandHandlerService
    {
        Task<CommandHandlingServiceResult> Handle(Command command, MessageDto message);
    }
}