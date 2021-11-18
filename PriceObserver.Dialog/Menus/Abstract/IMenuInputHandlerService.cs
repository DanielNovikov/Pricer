using System.Threading.Tasks;
using PriceObserver.Model.Dialog.Input;
using PriceObserver.Model.Dialog.Menu;

namespace PriceObserver.Dialog.Menus.Abstract
{
    public interface IMenuInputHandlerService
    {
        Task<MenuInputHandlingServiceResult> Handle(MessageDto message);
    }
}