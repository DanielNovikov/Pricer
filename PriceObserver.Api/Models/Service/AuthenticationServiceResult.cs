using PriceObserver.Api.Models.Response;
using PriceObserver.Common.Models;

namespace PriceObserver.Api.Models.Service;

public class AuthenticationServiceResult : 
    ServiceResult<
        AuthenticationServiceResult,
        AuthenticationResponseModel,
        AuthenticationErrorStatus>
{
}