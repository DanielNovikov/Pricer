using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.Service.Abstract;

public interface IMenuService
{
    string GetTitle(MenuKey key);
}