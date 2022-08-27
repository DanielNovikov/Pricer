using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Commands.Abstract;

public interface ICommandHandler
{
    public CommandKey Key { get; }
        
    public Task<CommandHandlingServiceResult> Handle(User user);
}