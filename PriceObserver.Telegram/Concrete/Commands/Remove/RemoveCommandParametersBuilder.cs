using PriceObserver.Model.Telegram;
using PriceObserver.Model.Telegram.Commands.Remove;
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

        public RemoveCommandParametersBuildResult Build(Update update)
        {
            var message = update.GetMessageText();
            var parseResult = _commandParametersParser.Parse(message, MessageParameterType.Number);

            if (!parseResult.IsSuccess)
                return RemoveCommandParametersBuildResult.Fail(parseResult.Error);

            var id = (int) parseResult.Result[0];
            var parameters = new RemoveCommandParameters(id);

            return RemoveCommandParametersBuildResult.Success(parameters);
        }
    }
}