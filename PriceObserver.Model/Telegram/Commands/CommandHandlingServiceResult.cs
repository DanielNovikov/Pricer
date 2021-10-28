using PriceObserver.Model.Common;
using PriceObserver.Model.Telegram.Common;

namespace PriceObserver.Model.Telegram.Commands
{
    public class CommandHandlingServiceResult : 
        ServiceResult<
            CommandHandlingServiceResult, 
            ReplyResult,
            string>
    {
    }
}