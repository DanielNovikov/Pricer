using PriceObserver.Model.Common;

namespace PriceObserver.Model.Parser
{
    public class ContentValidatorResult : ServiceResult<ContentValidatorResult, string, string>
    {
        public static ContentValidatorResult PriceDoesNotExist()
        {
            return Fail("На странице нет цены");
        }
    }
}