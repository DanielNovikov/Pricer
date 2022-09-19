using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Background.Services.Abstract;

public interface IItemPriceService
{
    Task Change(Item item, int newPrice);
}