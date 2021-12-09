using System.Threading.Tasks;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Data.Service.Abstract
{
    public interface IResourceValues
    {
        string Get(ResourceKey key);
    }
}