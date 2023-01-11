using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Models;

namespace Pricer.Dialog.Menus.Abstract;

public interface IMenuInputHandler
{
    MenuKey Key { get; }

    ValueTask<MenuInputHandlingServiceResult> Handle(MessageModel message);
}