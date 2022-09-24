using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Common.Models.Input;

namespace Pricer.Dialog.Common.Services.Abstract;

public interface IMenuKeyboardBuilder
{
    MenuKeyboard Build(MenuKey menuKey);
}