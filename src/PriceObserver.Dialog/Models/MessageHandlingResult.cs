using PriceObserver.Common.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Models;

public class MessageHandlingResult : ServiceResult<MessageHandlingResult, IReplyResult, ResourceKey>
{
    public static MessageHandlingResult FromServiceResult(MenuInputHandlingServiceResult serviceResult)
    {
        return new MessageHandlingResult
        {
            IsSuccess = serviceResult.IsSuccess,
            Error = serviceResult.Error,
            Result = serviceResult.Result
        };
    }
        
    public static MessageHandlingResult FromServiceResult(CommandHandlingServiceResult serviceResult)
    {
        return new MessageHandlingResult
        {
            IsSuccess = serviceResult.IsSuccess,
            Error = serviceResult.Error,
            Result = serviceResult.Result
        };
    }
}