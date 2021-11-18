using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Abstract
{
    public interface IUpdateHandler
    {
        Task Handle(Update update);
    }
}