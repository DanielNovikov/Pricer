using PriceObserver.Model.Telegram;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Telegram.Abstract.Commands;
using PriceObserver.Telegram.Abstract.Commands.Remove;
using PriceObserver.Telegram.Extensions;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Concrete.Commands.Remove
{
    public class RemoveCommandParametersBuilder : IRemoveCommandParametersBuilder
    {
        private readonly ICommandParametersParser _commandParametersParser;

        public RemoveCommandParametersBuilder(ICommandParametersParser commandParametersParser)
        {
            _commandParametersParser = commandParametersParser;
        }

        public RemoveCommandParameters Build(Update update)
        {
            var message = update.GetMessageText();
            var parameters = _commandParametersParser.Parse(message, MessageParameterType.Number);

            return new RemoveCommandParameters
            {
                Id = (int) parameters[0]
            };
        }
    }
}