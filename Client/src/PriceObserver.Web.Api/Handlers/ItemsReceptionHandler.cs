using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using PriceObserver.Web.Api.Extensions;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;

namespace PriceObserver.Web.Api.Handlers;

[Authorize]
public class ItemsReceptionHandler : ItemsReception.ItemsReceptionBase
{
    private readonly IItemsReceptionHandlerService _receptionHandlerService;

    public ItemsReceptionHandler(IItemsReceptionHandlerService receptionHandlerService)
    {
        _receptionHandlerService = receptionHandlerService;
    }

    public override async Task<ItemsReceptionReply> Receive(Empty request, ServerCallContext context)
    {
        var userId = context.GetUserId();
        
        return await _receptionHandlerService.Receive(userId);
    }
}