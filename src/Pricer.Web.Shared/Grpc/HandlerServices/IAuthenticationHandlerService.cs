namespace Pricer.Web.Shared.Grpc.HandlerServices;

public interface IAuthenticationHandlerService
{
    Task<AuthenticationReply> Authenticate(Guid token);
}