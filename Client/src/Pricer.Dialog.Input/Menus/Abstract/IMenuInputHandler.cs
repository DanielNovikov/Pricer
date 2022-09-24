using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Input.Models;

namespace Pricer.Dialog.Input.Menus.Abstract;

public interface IMenuInputHandler
{
    MenuKey Key { get; }

    ValueTask<MenuInputHandlingServiceResult> Handle(MessageModel message);
}