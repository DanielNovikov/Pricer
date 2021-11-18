using PriceObserver.Model.Common;
using PriceObserver.Model.Dialog.Commands;
using PriceObserver.Model.Dialog.Common;
using PriceObserver.Model.Dialog.Menu;

namespace PriceObserver.Model.Dialog.Input
{
    public class InputHandlingServiceResult : ServiceResult<InputHandlingServiceResult, ReplyResult, string>
    {
        public static InputHandlingServiceResult FromServiceResult(MenuInputHandlingServiceResult serviceResult)
        {
            return new InputHandlingServiceResult
            {
                IsSuccess = serviceResult.IsSuccess,
                Error = serviceResult.Error,
                Result = ReplyResult.Reply(serviceResult.Result)
            };
        }
        
        public static InputHandlingServiceResult FromServiceResult(CommandHandlingServiceResult serviceResult)
        {
            return new InputHandlingServiceResult
            {
                IsSuccess = serviceResult.IsSuccess,
                Error = serviceResult.Error,
                Result = serviceResult.Result
            };
        }
    }
}