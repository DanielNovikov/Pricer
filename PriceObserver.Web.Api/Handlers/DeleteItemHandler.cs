using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using PriceObserver.Web.Api.Extensions;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;

namespace PriceObserver.Web.Api.Handlers;

[Authorize]
public class DeleteItemHandler : DeleteItem.DeleteItemBase
{
    private readonly IDeleteItemHandlerService _handlerService;

    public DeleteItemHandler(IDeleteItemHandlerService handlerService)
    {
        _handlerService = handlerService;
    }

    public override async Task<Empty> Delete(DeleteItemRequest request, ServerCallContext context)
    {
        var itemId = request.ItemId;
        var userId = context.GetUserId();

        await _handlerService.Handle(itemId, userId);
        
        return new Empty();
    }
}