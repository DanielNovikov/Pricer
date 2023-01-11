using Pricer.Common.Models;
using Pricer.Data.InMemory.Models.Enums;

namespace Pricer.Parser.Models;

public class ContentValidatorResult : ServiceErrorResult<ContentValidatorResult, ResourceKey>
{
    public static ContentValidatorResult NoItemInfo()
    {
        return Fail(ResourceKey.Parser_NoItemInfoOnPage);
    }
}