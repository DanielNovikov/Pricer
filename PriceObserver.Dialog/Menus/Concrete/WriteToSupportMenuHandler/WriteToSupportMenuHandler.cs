using System.Threading.Tasks;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Data.Service.Abstract;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Input.Models;
using PriceObserver.Dialog.Menus.Abstract;
using PriceObserver.Dialog.Menus.Models;

namespace PriceObserver.Dialog.Menus.Concrete.WriteToSupportMenuHandler
{
    public class WriteToSupportMenuHandler : IMenuInputHandler
    {
        private readonly IUserActionLogger _userActionLogger;
        private readonly IResourceService _resourceService;

        public WriteToSupportMenuHandler(
            IUserActionLogger userActionLogger,
            IResourceService resourceService)
        {
            _userActionLogger = userActionLogger;
            _resourceService = resourceService;
        }

        public MenuKey Type => MenuKey.Support;
        
        public Task<MenuInputHandlingServiceResult> Handle(MessageDto message)
        {
            _userActionLogger.LogWriteToSupport(message.User, message.Text);

            var responseMessage = _resourceService.Get(ResourceKey.Dialog_SupportReply);
            var result = MenuInputHandlingServiceResult.Success(responseMessage); 
            
            return Task.FromResult(result);
        }
    }
}