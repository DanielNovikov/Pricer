using System.Threading.Tasks;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Menus.Abstract;

public interface IMenuInputHandlerService
{
    Task<MenuInputHandlingServiceResult> Handle(MessageModel message);
}