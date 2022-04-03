using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Web.Api.Services.Abstract;
using PriceObserver.Web.Shared.Models;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.Api.Services.Concrete;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IShopRepository _shopRepository;
    private readonly IShopVmBuilder _shopVmBuilder;
    private readonly IItemVmBuilder _itemVmBuilder;

    public ItemService(
        IItemRepository itemRepository,
        IShopRepository shopRepository, 
        IShopVmBuilder shopVmBuilder, 
        IItemVmBuilder itemVmBuilder)
    {
        _itemRepository = itemRepository;
        _shopRepository = shopRepository;
        _shopVmBuilder = shopVmBuilder;
        _itemVmBuilder = itemVmBuilder;
    }

    public async Task<IList<ItemsVm>> GetByUserId(long userId)
    {
        var items = await _itemRepository.GetByUserId(userId);
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
            .Select(x =>
            {
                var shopVm = _shopVmBuilder.Build(x.Shop);
                var itemVms = x.Items
                    .Select(_itemVmBuilder.Build)
                    .ToList();

                return new ItemsVm(shopVm, itemVms);
            })
            .ToList();
    }

    public async Task Delete(int id, long userId)
    {
        var item = await _itemRepository.GetById(id);

        if (item == null)
            throw new InvalidOperationException($"Item with {id} could not be found");
        
        if (item.UserId != userId)
            throw new InvalidOperationException($"Item with id {id} belongs to another user");
            
        await _itemRepository.Delete(item);
    }
}