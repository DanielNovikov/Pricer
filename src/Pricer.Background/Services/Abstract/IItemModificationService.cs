using System.Threading.Tasks;
using Pricer.Data.Persistent.Models;
using Pricer.Parser.Models;

namespace Pricer.Background.Services.Abstract;

public interface IItemModificationService
{
    Task Modify(Item item, ParsedItem parsedItem);
}