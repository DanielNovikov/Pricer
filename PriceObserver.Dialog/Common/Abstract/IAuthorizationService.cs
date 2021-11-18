using System.Threading.Tasks;
using PriceObserver.Model.Dialog.Common;
using PriceObserver.Model.Dialog.Input;

namespace PriceObserver.Dialog.Common.Abstract
{
    public interface IAuthorizationService
    {
        Task<AuthorizationResult> Authorize(UpdateDto update);
    }
}