using System.Threading.Tasks;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Dialog.Input;
using PriceObserver.Model.Dialog.Menu;

namespace PriceObserver.Dialog.Menus.Abstract
{
    public interface IMenuInputHandler
    {
        MenuType Type { get; }

        Task<MenuInputHandlingServiceResult> Handle(MessageDto message);
    }
}