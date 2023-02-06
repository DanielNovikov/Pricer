using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;
using Pricer.Parser.Models;

namespace Pricer.Background.Services.Abstract;

public interface IItemJobService
{
    ValueTask UpdateIsAvailable(Item item, bool isAvailable);
    
    Task Update(Item item, ParsedItem parsedItem);
    
    Task Remove(Item item, ResourceKey error);
}