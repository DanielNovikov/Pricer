using Pricer.Common.Models;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Dialog.Common.Models.Abstract;

namespace Pricer.Dialog.Input.Models;

public class CommandHandlingServiceResult : 
    ServiceResult<
        CommandHandlingServiceResult, 
        IReplyResult,
        ResourceKey>
{
}