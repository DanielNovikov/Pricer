using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Data.InMemory.Repositories.Abstract;

public interface IResourceRepository : IReadOnlyRepository<Resource, ResourceKey>
{
    Resource GetByValue(string value);
}