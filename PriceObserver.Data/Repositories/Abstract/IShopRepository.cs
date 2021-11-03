using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Model.Data;
using PriceObserver.Model.Data.Enums;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface IShopRepository
    {
        Task<Shop> GetByHost(string host);

        Task<IList<Shop>> GetAll();
    }
}