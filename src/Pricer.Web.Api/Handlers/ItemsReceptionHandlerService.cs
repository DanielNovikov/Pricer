using PriceObserver.Data.InMemory.Repositories.Abstract;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;
using Pricer.Common.Services.Abstract;
using Pricer.Web.Api.Services.Abstract;

namespace Pricer.Web.Api.Handlers;

public class ItemsReceptionHandlerService : IItemsReceptionHandlerService
{
    private readonly IItemRepository _itemRepository;
    private readonly IShopRepository _shopRepository;
    private readonly IShopResponseModelBuilder _shopResponseModelBuilder;
    private readonly IItemResponseModelBuilder _itemResponseModelBuilder;
    private readonly IUserLanguage _userLanguage;

    public ItemsReceptionHandlerService(
        IItemRepository itemRepository,
        IShopRepository shopRepository,
        IShopResponseModelBuilder shopResponseModelBuilder,
        IItemResponseModelBuilder itemResponseModelBuilder,
        IUserLanguage userLanguage)
    {
        _itemRepository = itemRepository;
        _shopRepository = shopRepository;
        _shopResponseModelBuilder = shopResponseModelBuilder;
        _itemResponseModelBuilder = itemResponseModelBuilder;
        _userLanguage = userLanguage;
    }
    
    public async Task<ItemsReceptionReply> Receive(int userId)
    {
        await _userLanguage.SetForUser(userId);
        
        var userItems = await _itemRepository.GetByUserId(userId);
        var shops = _shopRepository.GetAll();
            
        var responseModels = userItems
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
                var shop = _shopResponseModelBuilder.Build(x.Shop);
                var items = x.Items.Select(_itemResponseModelBuilder.Build);

                var responseModel = new ShopItemsResponseModel
                {
                    Shop = shop
                };
                
                responseModel.Items.AddRange(items);

                return responseModel;
            })
            .ToList();

        var result = new ItemsReceptionReply();
        result.Data.AddRange(responseModels);

        return result;
    }
}