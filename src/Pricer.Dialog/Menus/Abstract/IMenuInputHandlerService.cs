using System.Threading.Tasks;
using Pricer.Dialog.Models;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Menus.Abstract;

public interface IMenuInputHandlerService
{
    Task<IReplyResult> Handle(MessageModel message);
}