using PriceObserver.Model.Telegram.Commands.Remove;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Abstract.Commands.Remove
{
    public interface IRemoveCommandParametersBuilder
    {
        RemoveCommandParametersBuildResult Build(Update update);
    }
}