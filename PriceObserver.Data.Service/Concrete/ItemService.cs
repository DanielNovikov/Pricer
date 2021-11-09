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
        private readonly IShopToShopWithItemsVMConverter _shopConverter;
        private readonly IItemToItemVMConverter _itemConverter;
        
        public ItemService(
            IItemRepository repository, 
            IShopToShopWithItemsVMConverter shopConverter, 
            IItemToItemVMConverter itemConverter)
        {
            _repository = repository;
            _shopConverter = shopConverter;
            _itemConverter = itemConverter;
        }

        public async Task<IList<ShopWithItemsVM>> GetGroupedByUserId(long userId)
        {
            var items = await _repository.GetByUserId(userId);

            var grouped = items.GroupBy(x => x.Shop);

            return grouped
                .Select(x =>
                {
                    var shopVM = _shopConverter.Convert(x.Key);

                    shopVM.Items = x
                        .Select(y => _itemConverter.Convert(y))
                        .ToList();
                    
                    return shopVM;
                })
                .ToList();
        }
    }
}