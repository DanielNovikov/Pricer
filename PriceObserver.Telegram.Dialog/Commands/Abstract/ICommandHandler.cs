using System.Threading.Tasks;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Commands;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Commands.Abstract
{
    public interface ICommandHandler
    {
        public CommandType Type { get; }
        
        public Task<CommandHandlingServiceResult> Handle(User user);
    }
}