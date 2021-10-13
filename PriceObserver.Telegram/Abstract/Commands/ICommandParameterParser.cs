using PriceObserver.Model.Telegram;
using PriceObserver.Model.Telegram.Commands;

namespace PriceObserver.Telegram.Abstract.Commands
{
    public interface ICommandParameterParser
    {
        CommandParameterParseResult Parse(string parameter, MessageParameterType type);
    }
}