using Pricer.Data.InMemory.Models;

namespace Pricer.Models;

public record ShopDto(string Name, string Url, string BackgroundColor, string FontColor, List<ItemDto> Items);

public static class ShopDtoExtensions
{
    public static ShopDto ToDto(this Shop shop, List<ItemDto> items)
    {
        var url = $"https://{shop.Host}";
        
        return new ShopDto(shop.Name, url, shop.BackgroundColor, shop.FontColor, items);
    }
}