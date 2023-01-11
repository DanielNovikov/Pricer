using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;

namespace Pricer.Background.Services.Abstract;

public interface IItemRemovalService
{
    Task Remove(Item item, ResourceKey error);
}