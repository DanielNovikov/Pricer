using PriceObserver.Data.InMemory.Models.Enums;
using Pricer.Common.Models;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Models;

public class CommandHandlingServiceResult : 
    ServiceResult<
        CommandHandlingServiceResult, 
        IReplyResult,
        ResourceKey>
{
}