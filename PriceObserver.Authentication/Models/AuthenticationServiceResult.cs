using PriceObserver.Common.Models;

namespace PriceObserver.Authentication.Models;

public class AuthenticationServiceResult : 
    ServiceResult<
        AuthenticationServiceResult,
        AuthenticationResponseModel,
        AuthenticationErrorStatus>
{
}