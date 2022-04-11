using Grpc.Core;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;

namespace PriceObserver.Web.Api.Handlers;

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
        
        return await _handlerService.Handle(token);
    }
}