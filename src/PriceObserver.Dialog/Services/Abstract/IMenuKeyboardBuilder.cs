using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IMenuKeyboardBuilder
{
    MenuKeyboard Build(MenuKey menuKey);
}