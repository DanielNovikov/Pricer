using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Common.Models;

namespace PriceObserver.Dialog.Commands.Concrete.HelpCommand
{
    public class HelpCommandHandler : ICommandHandler
    {
        private readonly IUserActionLogger _userActionLogger;
        private readonly IResourceService _resourceService;
        private readonly ICommandRepository _commandRepository;
        
        public HelpCommandHandler(
            IUserActionLogger userActionLogger,
            IResourceService resourceService, 
            ICommandRepository commandRepository)
        {
            _userActionLogger = userActionLogger;
            _resourceService = resourceService;
            _commandRepository = commandRepository;
        }

        public CommandType Type => CommandType.Help;
        
        public async Task<CommandHandlingServiceResult> Handle(User user)
        {
            _userActionLogger.LogHelpCalled(user);

            var addCommandTitle = await GetCommandTitle(CommandType.Add);
            var allItemsCommandTitle = await GetCommandTitle(CommandType.AllItems);
            var shopsCommandTitle = await GetCommandTitle(CommandType.Shops);
            var websiteCommandTitle = await GetCommandTitle(CommandType.Website);
            var writeToSupportCommandTitle = await GetCommandTitle(CommandType.WriteToSupport);

            var message = _resourceService.Get(
                ResourceKey.Dialog_Help,
                addCommandTitle,
                allItemsCommandTitle,
                shopsCommandTitle,
                websiteCommandTitle,
                writeToSupportCommandTitle);

            var result = ReplyResult.Reply(message);
            return CommandHandlingServiceResult.Success(result);
        }

        private async Task<string> GetCommandTitle(CommandType type)
        {
            return (await _commandRepository.GetByType(type)).Resource.Value;
        }
    }
}