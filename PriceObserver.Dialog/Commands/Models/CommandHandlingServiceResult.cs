using PriceObserver.Common.Models;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Dialog.Common.Models;

namespace PriceObserver.Dialog.Commands.Models;

public class CommandHandlingServiceResult : 
    ServiceResult<
        CommandHandlingServiceResult, 
        ReplyResult,
        ResourceKey>
{
}