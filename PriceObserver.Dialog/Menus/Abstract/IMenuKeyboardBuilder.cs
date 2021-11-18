using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Dialog.Common.Models;

namespace PriceObserver.Dialog.Menus.Abstract
{
    public interface IMenuKeyboardBuilder
    {
        Task<MenuKeyboard> Build(Menu menu);
    }
}