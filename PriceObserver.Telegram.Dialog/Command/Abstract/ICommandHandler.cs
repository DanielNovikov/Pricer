using System.Threading.Tasks;
using PriceObserver.Model.Data.Enums;
using PriceObserver.Model.Telegram.Commands;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Command.Abstract
{
    public interface ICommandHandler
    {
        public CommandType Type { get; }
        
        public Task<CommandHandlingServiceResult> Handle(Update update, User user);
    }
}