using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.Service.Abstract
{
    public interface IMenuService
    {
        string GetTitle(MenuKey key);
    }
}