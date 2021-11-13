using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Input;
using PriceObserver.Model.Telegram.Menu;

namespace PriceObserver.Telegram.Dialog.Menus.Abstract
{
    public interface IMenuInputHandlerService
    {
        Task<MenuInputHandlingServiceResult> Handle(MessageDto message);
    }
}