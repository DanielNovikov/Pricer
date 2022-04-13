using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;

namespace PriceObserver.Web.App.Services.Concrete;

public class AuthenticationHandlerService : IAuthenticationHandlerService
{
    private readonly Authentication.AuthenticationClient _client;

    public AuthenticationHandlerService(Authentication.AuthenticationClient client)
    {
        _client = client;
    }

    public async Task<AuthenticationReply> Authenticate(Guid token)
    {
        var request = new AuthenticationRequest
        {
            Token = token.ToString()
        };
        
        return await _client.AuthenticateAsync(request);
    }
}