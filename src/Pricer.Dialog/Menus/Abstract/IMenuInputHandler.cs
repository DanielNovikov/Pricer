using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Menus.Abstract;

public interface IMenuInputHandler
{
    MenuKey Key { get; }

    ValueTask<IReplyResult> Handle(MessageModel message);
}