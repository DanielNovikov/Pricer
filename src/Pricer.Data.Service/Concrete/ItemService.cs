using System;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;

namespace PriceObserver.Data.Service.Concrete;

public class ItemService : IItemService
{
    private readonly IItemRepository _repository;
    private readonly IItemPriceChangeRepository _priceChangeRepository;
        
    public ItemService(
        IItemRepository repository, 
        IItemPriceChangeRepository priceChangeRepository)
    {
        _repository = repository;
        _priceChangeRepository = priceChangeRepository;
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

    public async Task UpdateAvailability(Item item, bool isAvailable)
    {
        item.IsAvailable = isAvailable;
        await _repository.Update(item);
    }

    public async Task<Item> Create(
        int price, string title, Uri url, Uri imageUrl, int userId, ShopKey shopKey, bool isAvailable, CurrencyKey currencyKey)
    {
        var item = new Item
        {
            Price = price,
            Url = url,
            Title = title,
            ImageUrl = imageUrl,
            UserId = userId,
            ShopKey = shopKey,
            IsAvailable = isAvailable,
            CurrencyKey = currencyKey
        };

        await _repository.Add(item);

        return item;
    }

    public async Task Delete(Item item)
    {
        item.IsDeleted = true;
        await _repository.Update(item);
    }

    public async Task Restore(Item item)
    {
        item.IsDeleted = false;
        await _repository.Update(item);
    }
}