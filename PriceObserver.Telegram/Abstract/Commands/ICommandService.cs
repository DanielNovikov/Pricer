using System.Threading.Tasks;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Abstract.Commands
{
    public interface ICommandService
    {
        Task Process(Update update, User user);
    }
}