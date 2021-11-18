using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Data.Service.Models;

namespace PriceObserver.Data.Service.Concrete
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        private readonly IShopRepository _shopRepository;
        private readonly IItemPriceChangeRepository _priceChangeRepository;
        
        public ItemService(
            IItemRepository repository, 
            IShopRepository shopRepository,
            IItemPriceChangeRepository priceChangeRepository)
        {
            _repository = repository;
            _shopRepository = shopRepository;
            _priceChangeRepository = priceChangeRepository;
        }

        public async Task<IList<ShopVM>> GetGroupedByUserId(long userId)
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
                .Select(x => CreateShopVM(x.Shop, x.Items))
                .ToList();
        }

        private static ShopVM CreateShopVM(Shop shop, IList<Item> items)
        {
            var address = $"https://{shop.Host}";
            var logoUrl = shop.LogoUrl.ToString();

            var itemVMs = items
                .Select(y => y.ToVM())
                .ToList();

            return new ShopVM(address, logoUrl, itemVMs);
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

        public async Task Delete(int id, long userId)
        {
            var item = await _repository.GetById(id);

            if (item.UserId != userId)
                throw new InvalidOperationException();
            
            await _repository.Delete(item);
        }
    }
}