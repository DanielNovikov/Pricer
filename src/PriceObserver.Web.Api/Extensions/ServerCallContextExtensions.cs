using System.Security.Claims;
using Grpc.Core;

namespace PriceObserver.Web.Api.Extensions;

public static class ServerCallContextExtensions
{
    public static int GetUserId(this ServerCallContext context)
    {
        var user = context.GetHttpContext().User;
        var userId = user.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;

        return int.Parse(userId);
    }
}