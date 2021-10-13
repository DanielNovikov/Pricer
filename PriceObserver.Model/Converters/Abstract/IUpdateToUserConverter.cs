using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Model.Converters.Abstract
{
    public interface IUpdateToUserConverter : IConverter<Chat, User>
    {
    }
}