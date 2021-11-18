using System.Threading.Tasks;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Dialog.Input.Models;
using PriceObserver.Dialog.Menus.Models;

namespace PriceObserver.Dialog.Menus.Abstract
{
    public interface IMenuInputHandler
    {
        MenuType Type { get; }

        Task<MenuInputHandlingServiceResult> Handle(MessageDto message);
    }
}