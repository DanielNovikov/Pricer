using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Background.Services.Abstract;

public interface IItemPriceChanger
{
    Task Change(Item item, int oldPrice, int newPrice);
}