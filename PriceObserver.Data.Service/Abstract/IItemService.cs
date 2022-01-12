using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Models;
using PriceObserver.Data.Service.Models;

namespace PriceObserver.Data.Service.Abstract;

public interface IItemService
{
    Task<IList<ShopVm>> GetGroupedByUserId(long userId);

    Task UpdatePrice(Item item, int price);
        
    Task Delete(int id, long userId);
    
    Task<Item> Create(int price, string title, Uri url, Uri imageUrl, long userId, ShopKey shopKey);
}