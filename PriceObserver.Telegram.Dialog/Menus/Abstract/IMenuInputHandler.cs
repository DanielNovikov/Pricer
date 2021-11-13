using System.Threading.Tasks;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Input;
using PriceObserver.Model.Telegram.Menu;

namespace PriceObserver.Telegram.Dialog.Menus.Abstract
{
    public interface IMenuInputHandler
    {
        MenuType Type { get; }

        Task<MenuInputHandlingServiceResult> Handle(MessageDto message);
    }
}