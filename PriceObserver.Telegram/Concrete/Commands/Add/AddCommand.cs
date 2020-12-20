using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Telegram.Abstract.Commands;
using PriceObserver.Telegram.Abstract.Commands.Add;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Concrete.Commands.Add
{
    public class AddCommand : ICommand
    {
        private readonly IAddCommandService _addCommandService;

        public AddCommand(IAddCommandService addCommandService)
        {
            _addCommandService = addCommandService;
        }

        public string Name { get; } = "add";
        
        public async Task<CommandExecutionResult> Process(Update update, User user)
        {
            await _addCommandService.Process(update, user);

            return new CommandExecutionResult
            {
                Message = "Item added successfully" 
            };
        }
    }
}