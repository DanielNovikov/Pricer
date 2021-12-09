using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using IResourceService = System.ComponentModel.Design.IResourceService;

namespace PriceObserver.Data.Service.Concrete
{
    public class ResourceValues : IResourceValues
    {
        private IDictionary<ResourceKey, string> _resourceValues;

        private readonly IServiceProvider _serviceProvider;

        public ResourceValues(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string Get(ResourceKey key)
        {
            if (_resourceValues == null)
                Initialize();

            return _resourceValues[key];
        }

        private void Initialize()
        {
            using var scope = _serviceProvider.CreateScope();
            var repository = scope.ServiceProvider.GetService<IResourceRepository>();
            
            var resources = repository!.GetAll();

            _resourceValues = resources.ToDictionary(
                resource => resource.Key,
                resource => resource.Value);
        }
    }
}