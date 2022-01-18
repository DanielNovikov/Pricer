namespace PriceObserver.Api.Services.Models.Response;

public record ShopVm(string Address, string LogoFileName, string CurrencySign, IList<ItemVm> Items);