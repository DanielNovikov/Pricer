using System.Threading.Tasks;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface IMenuCommandRepository
    {
        Task<bool> HasPair(int menuId, int commandId);
    }
}