using System;
using System.Threading.Tasks;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Data.Persistent.Models;

namespace Pricer.Data.Service.Abstract;

public interface IItemService
{
    Task UpdatePrice(Item item, int price);
    
    Task UpdateAvailability(Item item, bool isAvailable);
    
    Task<Item> Create(
        int price, string title, Uri url, Uri imageUrl, int userId, ShopKey shopKey, bool isAvailable, CurrencyKey currencyKey);

    Task Delete(Item item);
    
    Task Restore(Item item);
}