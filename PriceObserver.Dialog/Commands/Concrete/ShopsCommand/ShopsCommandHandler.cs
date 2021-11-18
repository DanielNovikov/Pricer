using System.Threading.Tasks;
using PriceObserver.Dialog.Commands.Abstract;
using PriceObserver.Dialog.Common.Abstract;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Dialog.Commands;
using PriceObserver.Model.Dialog.Common;
using User = PriceObserver.Model.Data.User;

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