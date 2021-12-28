using System.Threading.Tasks;
using PriceObserver.Dialog.Common.Models;
using PriceObserver.Dialog.Input.Models;

namespace PriceObserver.Dialog.Input.Abstract;

public interface IAuthorizationService
{
    Task<AuthorizationResult> Authorize(UpdateDto update);
}