using System.Threading.Tasks;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface IMenuRepository
    {
        Task<Menu> GetDefault();
    }
}