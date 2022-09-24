using System.Threading.Tasks;
using PriceObserver.Background.Services.Abstract;
using PriceObserver.Data.Persistent.Models;
using PriceObserver.Data.Persistent.Repositories.Abstract;
using PriceObserver.Parser.Models;

namespace PriceObserver.Background.Services.Concrete;

public class ItemModificationService : IItemModificationService
{
    private readonly IItemPriceService _itemPriceService;
    private readonly IItemRepository _itemRepository;

    public ItemModificationService(
        IItemPriceService itemPriceService,
        IItemRepository itemRepository)
    {
        _itemPriceService = itemPriceService;
        _itemRepository = itemRepository;
    }

    public async Task Modify(Item item, ParsedItem parsedItem)
    {
        item.Title = parsedItem.Title;
        item.ImageUrl = parsedItem.ImageUrl;
        await _itemRepository.Update(item);
        
        var newPrice = parsedItem.Price;
        await _itemPriceService.Change(item, newPrice);
    }
}