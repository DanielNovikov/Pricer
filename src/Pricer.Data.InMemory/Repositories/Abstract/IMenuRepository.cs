using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Repositories.Abstract;

public interface IMenuRepository : IReadOnlyRepository<Menu, MenuKey>
{
    Menu GetDefault();
}