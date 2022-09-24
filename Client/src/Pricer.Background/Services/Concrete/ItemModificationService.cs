using System.Threading.Tasks;
using Pricer.Background.Services.Abstract;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Parser.Models;

namespace Pricer.Background.Services.Concrete;

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