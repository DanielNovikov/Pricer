using PriceObserver.Common.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Common.Models;
using PriceObserver.Dialog.Menus.Models;

namespace PriceObserver.Dialog.Input.Models
{
    public class InputHandlingServiceResult : ServiceResult<InputHandlingServiceResult, ReplyResult, ResourceKey>
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