using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Data.InMemory.Models
{
    public record Command(CommandKey Key, ResourceKey ResourceKey, Menu Menu, Menu MenuToRedirect);
}