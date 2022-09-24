using Pricer.Common.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Common.Models.Abstract;

namespace Pricer.Dialog.Input.Models;

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