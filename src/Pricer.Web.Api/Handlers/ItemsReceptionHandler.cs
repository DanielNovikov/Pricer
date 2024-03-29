﻿using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Pricer.Web.Shared.Grpc;
using Pricer.Web.Api.Extensions;
using Pricer.Web.Shared.Grpc.HandlerServices;

namespace Pricer.Web.Api.Handlers;

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