using PriceObserver.Web.Shared.Grpc;

namespace Pricer.Web.Shared.Grpc.HandlerServices;

public interface IAuthenticationHandlerService
{
    Task<AuthenticationReply> Authenticate(Guid token);
}