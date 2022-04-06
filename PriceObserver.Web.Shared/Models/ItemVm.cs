namespace PriceObserver.Web.Shared.Models;

public record ItemVm(
    int Id,
    string Title,
    int Price,
    string Url,
    string ImageUrl,
    string PriceChanges);