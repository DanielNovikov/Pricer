using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Common;
using Telegram.Bot.Types;
using User = PriceObserver.Model.Data.User;

namespace PriceObserver.Telegram.Dialog.Common.Abstract
{
    public interface IChatAuthorizationService
    {
        Task<AuthorizationResult> Authorize(Chat chat);
    }
}