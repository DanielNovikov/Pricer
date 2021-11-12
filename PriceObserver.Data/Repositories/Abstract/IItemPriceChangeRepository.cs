using System.Threading.Tasks;
using PriceObserver.Model.Data;

namespace PriceObserver.Data.Repositories.Abstract
{
    public interface IItemPriceChangeRepository
    {
        Task Add(ItemPriceChange itemPriceChange);
    }
}