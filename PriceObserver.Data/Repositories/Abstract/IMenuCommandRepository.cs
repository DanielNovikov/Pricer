using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface IMenuCommandRepository
    {
        Task<bool> HasPair(int menuId, int commandId);

        Task<IList<Command>> GetMenuCommands(int menuId);
    }
}