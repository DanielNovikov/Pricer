using Pricer.Data.Persistent.Models;

namespace Pricer.Service.Models;

public record GlobalItemViewModel(
    int Id, Uri Url, int Price, string Title, bool IsAvailable, bool IsDeleted, string Currency, User User);