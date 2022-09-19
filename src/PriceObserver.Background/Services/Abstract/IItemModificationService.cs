using System.Threading.Tasks;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Parser.Models;

namespace PriceObserver.Background.Services.Abstract;

public interface IItemModificationService
{
    Task Modify(Item item, ParsedItem parsedItem);
}