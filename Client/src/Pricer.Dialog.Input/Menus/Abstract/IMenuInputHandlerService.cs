using Pricer.Dialog.Input.Models;

namespace Pricer.Dialog.Input.Menus.Abstract;

public interface IMenuInputHandlerService
{
    Task<MenuInputHandlingServiceResult> Handle(MessageModel message);
}