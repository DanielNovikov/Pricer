using System.Threading.Tasks;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Dialog.Commands;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Dialog.Commands.Abstract
{
    public interface ICommandHandler
    {
        public CommandType Type { get; }
        
        public Task<CommandHandlingServiceResult> Handle(User user);
    }
}