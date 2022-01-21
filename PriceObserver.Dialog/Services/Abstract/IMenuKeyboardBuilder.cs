using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IMenuKeyboardBuilder
{
    Task<MenuKeyboard> Build(MenuKey menuKey);
}