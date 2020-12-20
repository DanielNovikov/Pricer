using PriceObserver.Model.Telegram;

namespace PriceObserver.Telegram.Abstract.Commands
{
    public interface ICommandParameterParser
    {
        object Parse(string parameter, MessageParameterType type);
    }
}