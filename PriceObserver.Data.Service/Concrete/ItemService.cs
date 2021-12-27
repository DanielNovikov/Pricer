using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models;
using PriceObserver.Data.InMemory.Repositories.Abstract;
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

        public async Task<IList<ShopVm>> GetGroupedByUserId(long userId)
        {
            var items = await _repository.GetByUserId(userId);
            var shops = _shopRepository.GetAll();
            
            return items
                .GroupBy(x => x.ShopKey)
                .OrderBy(x => x.Key)
                .Join(
                    shops,
                    x => x.Key,
                    x => x.Key,
                    (grouped, shop) => new
                    {
                        Shop = shop,
                        Items = grouped.ToList()
                    })
                .Select(x => CreateShopVM(x.Shop, x.Items))
                .ToList();
        }

        private static ShopVm CreateShopVM(Shop shop, IList<Item> items)
        {
            var address = $"https://{shop.Host}";
            var logoFileName = shop.LogoFileName;

            var itemVMs = items
                .Select(y =>
                {
                    var priceChanges = y.PriceChanges.Any() 
                        ? y.PriceChanges
                            .Select(z =>
                            {
                                var sign = z.NewPrice > z.OldPrice ? "📈" : "📉" ; 
                                
                                return $"{z.OldPrice} {sign}";
                            })
                            .Aggregate((a, b) => $"{a} {b}")
                        : "Нет истории";

                    return y.ToVm(priceChanges);
                })
                .ToList();

            return new ShopVm(address, logoFileName, itemVMs);
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