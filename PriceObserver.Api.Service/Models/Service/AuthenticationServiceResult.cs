using PriceObserver.Api.Services.Models.Response;
using PriceObserver.Common.Models;

namespace PriceObserver.Api.Services.Models.Service;

public class AuthenticationServiceResult : 
    ServiceResult<
        AuthenticationServiceResult,
        AuthenticationResponseModel,
        AuthenticationErrorStatus>
{
}