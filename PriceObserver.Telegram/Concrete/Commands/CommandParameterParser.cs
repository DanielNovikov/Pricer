using System;
using PriceObserver.Model.Telegram;
using PriceObserver.Telegram.Abstract.Commands;

namespace PriceObserver.Telegram.Concrete.Commands
{
    public class CommandParameterParser : ICommandParameterParser
    {
        public object Parse(string parameter, MessageParameterType type)
        {
            if (string.IsNullOrEmpty(parameter))
                throw new Exception("You have a missed parameter");
            
            switch (type)
            {
                case MessageParameterType.URL:
                {
                    if (!Uri.TryCreate(parameter, UriKind.Absolute, out var uri))
                        throw new Exception($"'{parameter}' - does not look like a url");

                    return uri;
                }
                case MessageParameterType.String:
                {
                    return parameter;
                }
                case MessageParameterType.Number:
                {
                    if (!int.TryParse(parameter, out var number))
                        throw new Exception($"'{parameter}' - is not a number");

                    return number;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}