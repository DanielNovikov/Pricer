using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Service;

namespace PriceObserver.Data.Service.Concrete
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        private readonly IShopRepository _shopRepository;
        private readonly IShopToShopWithItemsVMConverter _shopConverter;
        private readonly IItemToItemVMConverter _itemConverter;
        
        public ItemService(
            IItemRepository repository, 
            IShopRepository shopRepository,
            IShopToShopWithItemsVMConverter shopConverter, 
            IItemToItemVMConverter itemConverter)
        {
            _repository = repository;
            _shopRepository = shopRepository;
            _shopConverter = shopConverter;
            _itemConverter = itemConverter;
        }

        public async Task<IList<ShopWithItemsVM>> GetGroupedByUserId(long userId)
        {
            var items = await _repository.GetByUserId(userId);
            var shops = await _shopRepository.GetAll();
            
            return items
                .GroupBy(x => x.ShopId)
                .OrderBy(x => x.Key)
                .Join(
                    shops,
                    x => x.Key,
                    x => x.Id,
                    (grouped, shop) => new
                    {
                        Shop = shop,
                        Items = grouped.ToList()
                    })
                .Select(x =>
                {
                    var shopVM = _shopConverter.Convert(x.Shop);

                    shopVM.Items = x.Items
                        .Select(y => _itemConverter.Convert(y))
                        .ToList();
                    
                    return shopVM;
                })
                .ToList();
        }
    }
}