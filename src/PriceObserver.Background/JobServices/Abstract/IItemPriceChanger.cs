using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Background.JobServices.Abstract;

public interface IItemPriceChanger
{
    Task Change(Item item, int oldPrice, int newPrice);
}