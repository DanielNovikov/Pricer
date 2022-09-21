using PriceObserver.Common.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Models.Abstract;

namespace PriceObserver.Dialog.Models;

public class CommandHandlingServiceResult : 
    ServiceResult<
        CommandHandlingServiceResult, 
        IReplyResult,
        ResourceKey>
{
}