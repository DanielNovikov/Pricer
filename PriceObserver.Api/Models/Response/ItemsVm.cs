namespace PriceObserver.Api.Models.Response;

public record ItemsVm(ShopVm Shop, IList<ItemVm> Items);