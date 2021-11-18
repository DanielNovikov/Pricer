using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface IMenuCommandRepository
    {
        Task<bool> HasPair(int menuId, int commandId);

        Task<IList<Command>> GetCommandsByMenuId(int menuId);
    }
}