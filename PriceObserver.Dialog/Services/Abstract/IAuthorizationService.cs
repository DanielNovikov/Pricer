using System.Threading.Tasks;
using PriceObserver.Dialog.Services.Models;

namespace PriceObserver.Dialog.Services.Abstract;

public interface IAuthorizationService
{
    Task<AuthorizationResult> Authorize(UpdateServiceModel update);
}