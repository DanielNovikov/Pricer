using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Input.Models;

namespace Pricer.Dialog.Input.Commands.Abstract;

public interface ICommandHandler
{
    public CommandKey Key { get; }
        
    public ValueTask<CommandHandlingServiceResult> Handle(User user);
}