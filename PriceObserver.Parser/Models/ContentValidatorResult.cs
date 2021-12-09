using PriceObserver.Common.Models;
using PriceObserver.Data.Models.Enums;

namespace PriceObserver.Parser.Models
{
    public class ContentValidatorResult : ServiceResult<ContentValidatorResult, string, ResourceKey>
    {
        public static ContentValidatorResult PriceDoesNotExist()
        {
            return Fail(ResourceKey.Parser_NoPriceOnPage);
        }
    }
}