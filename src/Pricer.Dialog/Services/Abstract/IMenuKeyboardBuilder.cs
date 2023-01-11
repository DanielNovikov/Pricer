using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Models;

namespace Pricer.Dialog.Services.Abstract;

public interface IMenuKeyboardBuilder
{
    MenuKeyboard Build(MenuKey menuKey);
}