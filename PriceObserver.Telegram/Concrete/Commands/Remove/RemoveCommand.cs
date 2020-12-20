using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Telegram.Abstract.Commands;
using PriceObserver.Telegram.Abstract.Commands.Remove;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Concrete.Commands.Remove
{
    public class RemoveCommand : ICommand
    {
        private readonly IRemoveCommandService _removeCommandService;

        public RemoveCommand(IRemoveCommandService removeCommandService)
        {
            _removeCommandService = removeCommandService;
        }

        public string Name { get; } = "remove";
        
        public async Task<CommandExecutionResult> Process(Update update, User user)
        {
            await _removeCommandService.Process(update, user);
            
            return new CommandExecutionResult
            {
                Message = "Item has been deleted successfully"
            };
        }
    }
}