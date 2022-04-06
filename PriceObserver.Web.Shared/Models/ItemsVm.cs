namespace PriceObserver.Web.Shared.Models;

public record ItemsVm(ShopVm Shop, IList<ItemVm> Items);