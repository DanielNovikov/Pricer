﻿using Google.Protobuf.WellKnownTypes;
using PriceObserver.Web.App.Services.Abstract;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;

namespace PriceObserver.Web.App.Services.Concrete;

public class ItemsReceptionHandlerService : IItemsReceptionHandlerService
{
    private readonly ItemsReception.ItemsReceptionClient _client;
    private readonly IMetadataBuilder _metadataBuilder;
    
    public ItemsReceptionHandlerService(
        ItemsReception.ItemsReceptionClient client, 
        IMetadataBuilder metadataBuilder)
    {
        _client = client;
        _metadataBuilder = metadataBuilder;
    }
    
    public async Task<ItemsReceptionReply> Receive(int userId)
    {
        var metadata = await _metadataBuilder.Build();
        
        return await _client.ReceiveAsync(new Empty(), metadata);
    }
}