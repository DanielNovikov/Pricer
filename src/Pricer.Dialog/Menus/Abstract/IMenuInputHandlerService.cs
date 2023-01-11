using System.Threading.Tasks;
using Pricer.Dialog.Models;

namespace Pricer.Dialog.Menus.Abstract;

public interface IMenuInputHandlerService
{
    Task<MenuInputHandlingServiceResult> Handle(MessageModel message);
}