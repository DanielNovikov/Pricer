using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using Pricer.Dialog.Models;

namespace Pricer.Dialog.Commands.Abstract;

public interface ICommandHandler
{
    public CommandKey Key { get; }
        
    public ValueTask<CommandHandlingServiceResult> Handle(User user);
}