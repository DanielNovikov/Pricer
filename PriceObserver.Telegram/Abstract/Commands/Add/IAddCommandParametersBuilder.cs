using PriceObserver.Model.Telegram.Commands;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Abstract.Commands.Add
{
    public interface IAddCommandParametersBuilder
    {
        AddCommandParameters Build(Update update);
    }
}