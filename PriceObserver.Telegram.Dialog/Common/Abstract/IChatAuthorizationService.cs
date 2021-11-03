using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Common;
using Telegram.Bot.Types;

namespace PriceObserver.Telegram.Dialog.Common.Abstract
{
    public interface IChatAuthorizationService
    {
        Task<AuthorizationResult> Authorize(Chat chat);
    }
}