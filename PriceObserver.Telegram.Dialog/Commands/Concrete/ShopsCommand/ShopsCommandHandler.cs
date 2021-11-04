using System.Threading.Tasks;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Model.Telegram.Common;
using PriceObserver.Telegram.Dialog.Commands.Abstract;
using PriceObserver.Telegram.Dialog.Common.Abstract;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Commands.Concrete.ShopsCommand
{
    public class ShopsCommandHandler : ICommandHandler
    {
        private readonly IShopsInfoMessageBuilder _shopsInfoMessageBuilder;

        public ShopsCommandHandler(IShopsInfoMessageBuilder shopsInfoMessageBuilder)
        {
            _shopsInfoMessageBuilder = shopsInfoMessageBuilder;
        }

        public CommandType Type => CommandType.Shops;
        
        public async Task<CommandHandlingServiceResult> Handle(User user)
        {
            var shopsInfoMessage = await _shopsInfoMessageBuilder.Build();

            var result = ReplyResult.Reply(shopsInfoMessage);
            return CommandHandlingServiceResult.Success(result);
        }
    }
}