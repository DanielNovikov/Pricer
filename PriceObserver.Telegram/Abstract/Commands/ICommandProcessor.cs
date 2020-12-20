using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Commands;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Abstract.Commands
{
    public interface ICommandProcessor
    {
        Task<CommandExecutionResult> Process(Update update);
    }
}