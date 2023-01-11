using System.Threading.Tasks;
using Pricer.Data.Persistent.Models;

namespace Pricer.Background.Services.Abstract;

public interface IItemPriceService
{
    Task Change(Item item, int newPrice);
}