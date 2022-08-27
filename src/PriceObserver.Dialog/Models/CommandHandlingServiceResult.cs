using PriceObserver.Common.Models;
using PriceObserver.Data.InMemory.Models.Enums;

namespace PriceObserver.Dialog.Models;

public class CommandHandlingServiceResult : 
    ServiceResult<
        CommandHandlingServiceResult, 
        ReplyResult,
        ResourceKey>
{
}