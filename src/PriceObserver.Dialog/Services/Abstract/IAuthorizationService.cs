using System.Threading.Tasks;
using PriceObserver.Dialog.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IAuthorizationService
{
    Task<AuthorizationResult> Authorize(UpdateServiceModel update);
}