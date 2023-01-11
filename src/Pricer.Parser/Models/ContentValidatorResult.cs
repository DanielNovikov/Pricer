using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Common.Models;

namespace Pricer.Parser.Models;

public class ContentValidatorResult : ServiceErrorResult<ContentValidatorResult, ResourceKey>
{
    public static ContentValidatorResult NoItemInfo()
    {
        return Fail(ResourceKey.Parser_NoItemInfoOnPage);
    }
}