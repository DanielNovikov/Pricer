using PriceObserver.Model.Telegram.Commands;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Abstract.Commands.Remove
{
    public interface IRemoveCommandParametersBuilder
    {
        RemoveCommandParameters Build(Update update);
    }
}