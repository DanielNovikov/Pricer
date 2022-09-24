using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Repositories.Abstract;

public interface IMenuRepository : IReadOnlyRepository<Menu, MenuKey>
{
    Menu GetDefault();
}