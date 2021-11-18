using System.Threading.Tasks;
using PriceObserver.Data.Models;
using PriceObserver.Data.Models.Enums;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Commands.Models;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Dialog.Common.Models;

namespace PriceObserver.Dialog.Commands.Concrete.ShopsCommand
{
    public class ShopsCommandHandler : ICommandHandler
    {
        private readonly IShopsInfoMessageBuilder _shopsInfoMessageBuilder;
        private readonly IUserActionLogger _userActionLogger;
        
        public ShopsCommandHandler(
            IShopsInfoMessageBuilder shopsInfoMessageBuilder,
            IUserActionLogger userActionLogger)
        {
            _shopsInfoMessageBuilder = shopsInfoMessageBuilder;
            _userActionLogger = userActionLogger;
        }

        public CommandType Type => CommandType.Shops;
        
        public async Task<CommandHandlingServiceResult> Handle(User user)
        {
            _userActionLogger.LogShopsCalled(user);
            
            var shopsInfoMessage = await _shopsInfoMessageBuilder.Build();

            var result = ReplyResult.Reply(shopsInfoMessage);
            return CommandHandlingServiceResult.Success(result);
        }
    }
}