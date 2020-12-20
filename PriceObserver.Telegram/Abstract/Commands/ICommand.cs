using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Commands;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Abstract.Commands
{
    public interface ICommand
    {
        public string Name { get; }

        public Task<CommandExecutionResult> Process(Update update, User user);
    }
}