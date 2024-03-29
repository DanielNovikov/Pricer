﻿using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Pricer.Web.Shared.Grpc;
using Pricer.Web.Api.Extensions;
using Pricer.Web.Shared.Grpc.HandlerServices;

namespace Pricer.Web.Api.Handlers;

[Authorize]
public class ItemDeletionHandler : ItemDeletion.ItemDeletionBase
{
	private readonly IItemDeletionHandlerService _deletionHandlerService;

	public ItemDeletionHandler(IItemDeletionHandlerService deletionHandlerService)
	{
		_deletionHandlerService = deletionHandlerService;
	}

	public override async Task<Empty> Delete(ItemDeletionRequest request, ServerCallContext context)
	{
		var itemId = request.ItemId;
		var userId = context.GetUserId();

		await _deletionHandlerService.Delete(itemId, userId);

		return new Empty();
	}
}