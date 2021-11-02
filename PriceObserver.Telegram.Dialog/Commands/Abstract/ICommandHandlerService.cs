using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Commands;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Commands.Abstract
{
    public interface ICommandHandlerService
    {
        Task<CommandHandlingServiceResult> Handle(Model.Data.Command command, Update update, User user);
    }
}