using System.Threading.Tasks;
using PriceObserver.Dialog.Input.Models;
using PriceObserver.Dialog.Menus.Models;

namespace PriceObserver.Dialog.Menus.Abstract;

public interface IMenuInputHandlerService
{
    Task<MenuInputHandlingServiceResult> Handle(MessageDto message);
}