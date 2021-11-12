using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Data;
using PriceObserver.Model.Service;

namespace PriceObserver.Data.Service.Concrete
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        private readonly IShopRepository _shopRepository;
        private readonly IShopToShopWithItemsVMConverter _shopConverter;
        private readonly IItemToItemVMConverter _itemConverter;
        private readonly IItemPriceChangeRepository _priceChangeRepository;
        
        public ItemService(
            IItemRepository repository, 
            IShopRepository shopRepository,
            IShopToShopWithItemsVMConverter shopConverter, 
            IItemToItemVMConverter itemConverter, 
            IItemPriceChangeRepository priceChangeRepository)
        {
            _repository = repository;
            _shopRepository = shopRepository;
            _shopConverter = shopConverter;
            _itemConverter = itemConverter;
            _priceChangeRepository = priceChangeRepository;
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

        public async Task UpdatePrice(Item item, int price)
        {
            var priceChange = new ItemPriceChange
            {
                Created = DateTime.UtcNow,
                ItemId = item.Id,
                NewPrice = price,
                OldPrice = item.Price
            };

            await _priceChangeRepository.Add(priceChange);

            item.Price = price;
            await _repository.Update(item);
        }

        public async Task Delete(int id)
        {
            var item = await _repository.GetById(id);
            await _repository.Delete(item);
        }
    }
}