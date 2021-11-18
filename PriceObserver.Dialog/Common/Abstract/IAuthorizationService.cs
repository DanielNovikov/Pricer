using System.Threading.Tasks;
using PriceObserver.Model.Telegram.Common;
using PriceObserver.Model.Telegram.Input;

namespace PriceObserver.Dialog.Common.Abstract
{
    public interface IAuthorizationService
    {
        Task<AuthorizationResult> Authorize(UpdateDto update);
    }
}