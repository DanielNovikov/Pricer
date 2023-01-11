using Google.Protobuf.WellKnownTypes;
using Pricer.Web.Shared.Grpc;
using Pricer.Web.App.Services.Abstract;
using Pricer.Web.Shared.Grpc.HandlerServices;

namespace Pricer.Web.App.Services.Concrete;

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