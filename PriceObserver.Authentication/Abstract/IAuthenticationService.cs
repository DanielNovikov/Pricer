using System;
using System.Threading.Tasks;
using PriceObserver.Authentication.Models;

namespace PriceObserver.Authentication.Abstract;

public interface IAuthenticationService
{
    Task<AuthenticationServiceResult> Authenticate(Guid token);
}