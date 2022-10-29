using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Service.Models;

public record ItemViewModel(
    int Id, Uri Url, int Price, string Title, ShopKey ShopKey, bool IsAvailable, bool IsDeleted, string Currency);