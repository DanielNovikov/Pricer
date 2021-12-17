using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Common.Models;

namespace PriceObserver.Dialog.Menus.Abstract
{
    public interface IMenuKeyboardBuilder
    {
        Task<MenuKeyboard> Build(MenuKey menuKey);
    }
}