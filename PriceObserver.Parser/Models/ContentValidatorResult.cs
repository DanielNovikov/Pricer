using PriceObserver.Common.Models;

namespace PriceObserver.Parser.Models
{
    public class ContentValidatorResult : ServiceResult<ContentValidatorResult, string, string>
    {
        public static ContentValidatorResult PriceDoesNotExist()
        {
            return Fail("На странице нет цены");
        }
    }
}