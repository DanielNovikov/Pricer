using PriceObserver.Model.Telegram;
using PriceObserver.Model.Telegram.Commands;

namespace PriceObserver.Telegram.Abstract.Commands
{
    public interface ICommandParametersParser
    {
        CommandParametersParseResult Parse(string message, params MessageParameterType[] parameterTypes);
    }
}