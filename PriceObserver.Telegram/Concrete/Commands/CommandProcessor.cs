using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PriceObserver.Data.Repositories.Abstract;
using PriceObserver.Model.Converters.Abstract;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Telegram.Abstract.Commands;
using PriceObserver.Telegram.Extensions;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Concrete.Commands
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IEnumerable<ICommand> _commands;
        private readonly IUserRepository _userRepository;
        private readonly IUpdateToUserConverter _updateToUserConverter;

        public CommandProcessor(
            IEnumerable<ICommand> commands,
            IUserRepository userRepository, 
            IUpdateToUserConverter updateToUserConverter)
        {
            _commands = commands;
            _userRepository = userRepository;
            _updateToUserConverter = updateToUserConverter;
        }

        public async Task<CommandExecutionResult> Process(Update update)
        {
            var message = update.GetMessageText();
            
            var command = _commands.FirstOrDefault(x => message.StartsWith($"/{x.Name}"));
            if (command == null)
                return CommandExecutionResult.Fail("Wrong command");

            var userId = update.GetUserId();
            var user = await _userRepository.GetById(userId);

            if (user == null)
            {
                var chat = update.Message.Chat;
                user = _updateToUserConverter.Convert(chat);
                await _userRepository.Add(user);
            }
            
            return await command.Process(update, user);
        }
    }
}