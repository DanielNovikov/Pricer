using PriceObserver.Data.Models;

namespace PriceObserver.Data.Service.Models
{
    public record ItemVm(int Id, string Title, int Price, string Url, string ImageUrl, string PriceChanges);

    public static class ItemVmExtensions
    {
        public static ItemVm ToVm(this Item item, string priceChanges)
        {
            return new ItemVm(
                item.Id,
                item.Title,
                item.Price,
                item.Url.ToString(),
                item.ImageUrl.ToString(),
                priceChanges);
        }
    }
}