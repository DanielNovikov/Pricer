using PriceObserver.Api.Services.Abstract;
using PriceObserver.Api.Services.Models.Response;
using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Repositories.Abstract;

namespace PriceObserver.Api.Services.Concrete;

public class ApiItemService : IApiItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IShopRepository _shopRepository;
    private readonly IShopVmBuilder _shopVmBuilder;

    public ApiItemService(
        IItemRepository itemRepository,
        IShopRepository shopRepository, 
        IShopVmBuilder shopVmBuilder)
    {
        _itemRepository = itemRepository;
        _shopRepository = shopRepository;
        _shopVmBuilder = shopVmBuilder;
    }

    public async Task<IList<ShopVm>> GetByUserId(long userId)
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
            .Select(x => _shopVmBuilder.Build(x.Shop, x.Items))
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