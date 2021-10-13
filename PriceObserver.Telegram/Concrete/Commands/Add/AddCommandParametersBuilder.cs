using System;
using PriceObserver.Model.Telegram;
using PriceObserver.Model.Telegram.Commands.Add;
using PriceObserver.Telegram.Abstract.Commands;
using PriceObserver.Telegram.Abstract.Commands.Add;
using PriceObserver.Telegram.Extensions;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Concrete.Commands.Add
{
    public class AddCommandParametersBuilder : IAddCommandParametersBuilder
    {
        private readonly ICommandParametersParser _commandParametersParser;

        public AddCommandParametersBuilder(ICommandParametersParser commandParametersParser)
        {
            _commandParametersParser = commandParametersParser;
        }

        public AddCommandParametersBuildResult Build(Update update)
        {
            var message = update.GetMessageText();
            var parseResult = _commandParametersParser.Parse(message, MessageParameterType.URL);

            if (!parseResult.IsSuccess)
                return AddCommandParametersBuildResult.Fail(parseResult.Error);

            var url = (Uri) parseResult.Result[0];
            var parameters = new AddCommandParameters(url);

            return AddCommandParametersBuildResult.Success(parameters);
        }
    }
}