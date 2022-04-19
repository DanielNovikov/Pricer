using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Data.Persistent.Repositories.Abstract;

public interface IItemPriceChangeRepository
{
    Task Add(ItemPriceChange itemPriceChange);
}