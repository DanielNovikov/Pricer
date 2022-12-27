﻿using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Service.Models;
using Pricer.Service.Services.Abstract;

namespace Pricer.Service.Services.Concrete;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IShopRepository _shopRepository;
    
    public ItemService(
        IItemRepository itemRepository,
        IShopRepository shopRepository)
    {
        _itemRepository = itemRepository;
        _shopRepository = shopRepository;
    }

    public async Task<GlobalItemViewModel[]> GetAll()
    {
        var items = await _itemRepository.GetAllIncludingUser();
        
        return items
            .Select(x =>
            {
                var shop = _shopRepository.GetByKey(x.ShopKey);
                var title = $"{shop.Name}: {x.Title}";

                var currency = x.CurrencyKey.ToString();
                
                return new GlobalItemViewModel(x.Id, x.Url, x.Price, title, x.IsAvailable, x.IsDeleted, currency, x.User);
            })
            .OrderBy(x => x.User.Id)
            .ToArray();
    }

    public async Task<UserItemViewModel[]> GetByUserId(int userId)
    {
        var items = await _itemRepository.GetByUserId(userId);

        return items
            .Select(x =>
            {
                var shop = _shopRepository.GetByKey(x.ShopKey);
                var title = $"{shop.Name}: {x.Title}";

                var currency = x.CurrencyKey.ToString();

                return new UserItemViewModel(x.Id, x.Url, x.Price, title, x.IsAvailable, x.IsDeleted, currency);
            })
            .ToArray();
    }

    public async Task DeleteItem(int id)
    {
        var item = await _itemRepository.GetById(id);

        await _itemRepository.Delete(item);
    }
}