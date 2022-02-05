namespace PriceObserver.Api.Models.Response;

public record ShopVm(
    string Address,
    string Logo,
    string CurrencySign,
    bool SameFormatImages);