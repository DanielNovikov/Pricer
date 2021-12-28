using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Repositories.Abstract;

public interface IResourceRepository
{
    Resource GetByKey(ResourceKey key);

    Resource GetByValue(string value);
}