using System.Threading.Tasks;
using PriceObserver.Model.Data;
using PriceObserver.Model.Dialog.Common;

namespace PriceObserver.Dialog.Menus.Abstract
{
    public interface IMenuKeyboardBuilder
    {
        Task<MenuKeyboard> Build(Menu menu);
    }
}