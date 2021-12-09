using System;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Common.Abstract;

namespace PriceObserver.Dialog.Common.Concrete
{
    public class ShopsInfoMessageBuilder : IShopsInfoMessageBuilder
    {
        private readonly IShopRepository _shopRepository;
        private readonly IResourceService _resourceService;

        public ShopsInfoMessageBuilder(
            IShopRepository shopRepository,
            IResourceService resourceService)
        {
            _shopRepository = shopRepository;
            _resourceService = resourceService;
        }

        public async Task<string> Build()
        {
            var shops = await _shopRepository.GetAll();
            
            var shopsInfo = shops
                .Select(x => $"- {x.Name} ({x.Host})")
                .Aggregate((x, y) => $"{x}{Environment.NewLine}{y}");

            return _resourceService.Get(ResourceKey.Dialog_AvailableShops, shopsInfo);
        }
    }
}