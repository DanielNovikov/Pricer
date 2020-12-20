using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Telegram.Abstract.Commands;
using PriceObserver.Telegram.Abstract.Commands.All;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Concrete.Commands.All
{
    public class AllCommand : ICommand
    {
        private readonly IAllCommandService _allCommandService;

        public AllCommand(IAllCommandService allCommandService)
        {
            _allCommandService = allCommandService;
        }

        public string Name => "all";
        
        public async Task<CommandExecutionResult> Process(Update update, User user)
        {
            await _allCommandService.Process(update, user);

            return new CommandExecutionResult
            {
                Message = "All items listed below"
            };
        }
    }
}