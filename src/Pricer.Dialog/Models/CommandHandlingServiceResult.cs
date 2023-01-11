using Pricer.Common.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Models.Abstract;

namespace Pricer.Dialog.Models;

public class CommandHandlingServiceResult : 
    ServiceResult<
        CommandHandlingServiceResult, 
        IReplyResult,
        ResourceKey>
{
}