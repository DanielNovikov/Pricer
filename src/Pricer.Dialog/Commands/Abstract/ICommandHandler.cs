using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Commands.Abstract;

public interface ICommandHandler
{
    public CommandKey Key { get; }
        
    public ValueTask<IReplyResult> Handle(User user);
}