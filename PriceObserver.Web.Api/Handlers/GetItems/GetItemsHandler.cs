using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using PriceObserver.Web.Api.Extensions;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;

namespace PriceObserver.Web.Api.Handlers.GetItems;

[Authorize]
public class GetItemsHandler : Shared.Grpc.GetItems.GetItemsBase
{
    private readonly IGetItemsHandlerService _handlerService;

    public GetItemsHandler(IGetItemsHandlerService handlerService)
    {
        _handlerService = handlerService;
    }

    public override async Task<GetItemsReply> Get(Empty request, ServerCallContext context)
    {
        var userId = context.GetUserId();
        
        return await _handlerService.Handle(userId);
    }
}