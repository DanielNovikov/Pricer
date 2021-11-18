using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Input;
using PriceObserver.Model.Telegram.Menu;

namespace PriceObserver.Dialog.Menus.Abstract
{
    public interface IMenuInputHandlerService
    {
        Task<MenuInputHandlingServiceResult> Handle(MessageDto message);
    }
}