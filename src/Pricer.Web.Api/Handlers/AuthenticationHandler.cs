using Grpc.Core;
using Pricer.Web.Shared.Grpc;
using Pricer.Web.Shared.Grpc.HandlerServices;

namespace Pricer.Web.Api.Handlers;

public class AuthenticationHandler : Authentication.AuthenticationBase
{
    private readonly IAuthenticationHandlerService _handlerService;

    public AuthenticationHandler(IAuthenticationHandlerService handlerService)
    {
        _handlerService = handlerService;
    }

    public override async Task<AuthenticationReply> Authenticate(
        AuthenticationRequest request,
        ServerCallContext context)
    {
        if (!Guid.TryParse(request.Token, out var token))
            throw new InvalidOperationException($"Can't parse '{request.Token}'");
        
        return await _handlerService.Authenticate(token);
    }
}