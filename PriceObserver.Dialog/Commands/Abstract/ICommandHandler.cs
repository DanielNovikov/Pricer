using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Dialog.Commands.Models;

namespace PriceObserver.Dialog.Commands.Abstract
{
    public interface ICommandHandler
    {
        public CommandType Type { get; }
        
        public Task<CommandHandlingServiceResult> Handle(User user);
    }
}