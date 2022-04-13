using System.Security.Claims;
using Grpc.Core;

namespace PriceObserver.Web.Api.Extensions;

public static class ServerCallContextExtensions
{
    public static long GetUserId(this ServerCallContext context)
    {
        var user = context.GetHttpContext().User;
        var userId = user.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;

        return long.Parse(userId);
    }
}