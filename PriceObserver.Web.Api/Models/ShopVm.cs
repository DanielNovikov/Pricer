namespace PriceObserver.Web.Api.Models;

public record ShopVm(
    string Address,
    string Logo,
    string CurrencySign,
    bool SameFormatImages);