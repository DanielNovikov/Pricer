using System;
using System.Threading.Tasks;
using PriceObserver.Model.Authentication;

namespace PriceObserver.Authentication.Abstract
{
    public interface IAuthenticationService
    {
        Task<AuthenticationServiceResult> Authenticate(Guid token);
    }
}