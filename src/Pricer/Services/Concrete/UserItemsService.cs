using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Extensions;
using Pricer.Models;
using Pricer.Services.Abstract;

namespace Pricer.Services.Concrete;

public class UserItemsService : IUserItemsService
{
    private readonly IItemRepository _itemRepository;
    private readonly IShopRepository _shopRepository;
    private readonly IItemPriceChangeRepository _itemPriceChangeRepository;
    private readonly ICurrencyService _currencyService;

    public UserItemsService(
        IItemRepository itemRepository,
        IShopRepository shopRepository,
        IItemPriceChangeRepository itemPriceChangeRepository,
        ICurrencyService currencyService)
    {
        _itemRepository = itemRepository;
        _shopRepository = shopRepository;
        _itemPriceChangeRepository = itemPriceChangeRepository;
        _currencyService = currencyService;
    }

    public async Task<List<ShopDto>> GetByUserId(int userId)
    {
        var items = await _itemRepository.GetByUserId(userId);

        return items
            .Where(x => !x.IsDeleted)
            .GroupBy(x => x.ShopKey)
            .Select(x =>
            {
                var shop = _shopRepository.GetByKey(x.Key);
                var itemsDto = x
                    .Select(y =>
                    {
                        var currencySign = _currencyService.GetSign(y.CurrencyKey);
                        return new ItemDto(y.Id, y.Url, y.Price, y.Title, y.ImageUrl, currencySign);
                    })
                    .ToList();

                return shop.ToDto(itemsDto);
            })
            .ToList();
    }

    public async Task<List<ItemPriceChangeDto>> GetPriceChanges(int id)
    {
        var priceChanges = await _itemPriceChangeRepository.GetByItemId(id, 8);
        var currencyKey = await _itemRepository.GetCurrency(id);
        var currencySign = _currencyService.GetSign(currencyKey);
        
        return priceChanges
            .Select(x => new ItemPriceChangeDto(x.Created.ToRelative(), x.OldPrice, x.NewPrice, currencySign))
            .ToList();
    }
}