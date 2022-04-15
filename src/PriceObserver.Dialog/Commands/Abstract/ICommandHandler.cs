using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;
using PriceObserver.Dialog.Commands.Models;

namespace PriceObserver.Dialog.Commands.Abstract;

public interface ICommandHandler
{
    public CommandKey Type { get; }
        
    public Task<CommandHandlingServiceResult> Handle(User user);
}