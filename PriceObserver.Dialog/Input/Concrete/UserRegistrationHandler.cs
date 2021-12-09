using System.Threading.Tasks;
using PriceObserver.Common.Extensions;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Common.Models;
using PriceObserver.Dialog.Input.Abstract;
using PriceObserver.Dialog.Menus.Abstract;

namespace PriceObserver.Dialog.Input.Concrete
{
    public class UserRegistrationHandler : IUserRegistrationHandler
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMenuKeyboardBuilder _menuKeyboardBuilder;
        private readonly IShopsInfoMessageBuilder _shopsInfoMessageBuilder;
        private readonly IUserActionLogger _userActionLogger;
        private readonly IResourceService _resourceService;
        
        public UserRegistrationHandler(
            ICommandRepository commandRepository,
            IMenuKeyboardBuilder menuKeyboardBuilder, 
            IShopsInfoMessageBuilder shopsInfoMessageBuilder, 
            IUserActionLogger userActionLogger, 
            IResourceService resourceService)
        {
            _commandRepository = commandRepository;
            _menuKeyboardBuilder = menuKeyboardBuilder;
            _shopsInfoMessageBuilder = shopsInfoMessageBuilder;
            _userActionLogger = userActionLogger;
            _resourceService = resourceService;
        }

        public async Task<ReplyResult> Handle(User user)
        {
            _userActionLogger.LogUserRegistered(user);
            
            var addCommandTitle = await GetCommandTitle(CommandType.Add);
            var websiteCommandTitle = await GetCommandTitle(CommandType.Website);
            var writeToSupportTitle = await GetCommandTitle(CommandType.WriteToSupport);
            
            var shopsInfoMessage = await _shopsInfoMessageBuilder.Build();

            var message = _resourceService.Get(
                ResourceKey.Dialog_UserRegistered,
                user.GetFullName(),
                addCommandTitle,
                websiteCommandTitle,
                writeToSupportTitle,
                shopsInfoMessage,
                user.Menu.Text);

            var menuKeyboard = await _menuKeyboardBuilder.Build(user.Menu);

            return ReplyResult.ReplyWithKeyboard(message, menuKeyboard);
        }

        private async Task<string> GetCommandTitle(CommandType type)
        {
            return (await _commandRepository.GetByType(type)).Title;
        }
    }
}