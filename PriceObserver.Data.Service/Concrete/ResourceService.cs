using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Data.Service.Concrete
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceValues _resourceValues;

        public ResourceService(IResourceValues resourceValues)
        {
            _resourceValues = resourceValues;
        }

        public string Get(ResourceKey key, params object[] parameters)
        {
            var resourceValue = _resourceValues.Get(key);

            return string.Format(resourceValue, parameters);
        }
    }
}