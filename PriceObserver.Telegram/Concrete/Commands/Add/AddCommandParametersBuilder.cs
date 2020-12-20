using System;
using PriceObserver.Model.Telegram;
using PriceObserver.Model.Telegram.Commands;
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

        public AddCommandParameters Build(Update update)
        {
            var message = update.GetMessageText();
            var parameters = _commandParametersParser.Parse(message, MessageParameterType.URL);

            return new AddCommandParameters
            {
                Url = (Uri) parameters[0] 
            };
        }
    }
}