using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;

namespace PriceObserver.Background.Services.Abstract;

public interface IItemRemovalService
{
    Task Remove(Item item, ResourceKey error);
}