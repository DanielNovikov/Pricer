using System.Threading.Tasks;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface IMenuRepository
    {
        Task<Menu> GetDefault();
    }
}