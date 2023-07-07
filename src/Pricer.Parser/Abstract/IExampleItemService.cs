using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Models;

namespace Pricer.Parser.Abstract;

public interface IExampleItemService
{
    ShopKey ShopKey { get; }
    
    Task<ExampleResult> Get();
}