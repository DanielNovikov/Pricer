using System.Threading.Tasks;
using PriceObserver.Data.Models;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface IItemPriceChangeRepository
    {
        Task Add(ItemPriceChange itemPriceChange);
    }
}