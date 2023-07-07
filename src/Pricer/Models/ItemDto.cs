using Pricer.Data.Persistent.Models;

namespace Pricer.Models;

public record ItemDto(int Id, Uri Url, int Price, string Title, Uri ImageUrl, string CurrencySign);