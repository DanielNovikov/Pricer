using PriceObserver.Model.Telegram.Commands.Add;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Abstract.Commands.Add
{
    public interface IAddCommandParametersBuilder
    {
        AddCommandParametersBuildResult Build(Update update);
    }
}