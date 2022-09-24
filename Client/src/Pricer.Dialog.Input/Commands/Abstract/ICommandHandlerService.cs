using Pricer.Data.InMemory.Models;
using Pricer.Dialog.Input.Models;

namespace Pricer.Dialog.Input.Commands.Abstract;

public interface ICommandHandlerService
{
    Task<CommandHandlingServiceResult> Handle(Command command, MessageModel message);
}