using System.Threading.Tasks;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Common.Abstract
{
    public interface IChatService
    {
        Task<User> GetUser(Chat chat);
    }
}