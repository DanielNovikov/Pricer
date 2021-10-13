using System;
using PriceObserver.Model.Telegram;
using PriceObserver.Model.Telegram.Commands;
using PriceObserver.Telegram.Abstract.Commands;

namespace PriceObserver.Telegram.Concrete.Commands
{
    public class CommandParameterParser : ICommandParameterParser
    {
        public CommandParameterParseResult Parse(string parameter, MessageParameterType type)
        {
            if (string.IsNullOrEmpty(parameter))
                return CommandParameterParseResult.Fail("You have a missed parameter");
            
            switch (type)
            {
                case MessageParameterType.URL:
                {
                    if (!Uri.TryCreate(parameter, UriKind.Absolute, out var uri))
                        return CommandParameterParseResult.Fail($"'{parameter}' - does not look like a url");

                    return CommandParameterParseResult.Success(uri);
                }
                case MessageParameterType.String:
                {
                    return CommandParameterParseResult.Success(parameter);
                }
                case MessageParameterType.Number:
                {
                    if (!int.TryParse(parameter, out var number))
                        return CommandParameterParseResult.Fail($"'{parameter}' - is not a number");

                    return CommandParameterParseResult.Success(number);
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}