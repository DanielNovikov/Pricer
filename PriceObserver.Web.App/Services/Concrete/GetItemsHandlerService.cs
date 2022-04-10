using Google.Protobuf.WellKnownTypes;
using PriceObserver.Web.App.Services.Abstract;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;

namespace PriceObserver.Web.App.Services.Concrete;

public class GetItemsHandlerService : IGetItemsHandlerService
{
    private readonly GetItems.GetItemsClient _getItemsClient;
    private readonly IMetadataBuilder _metadataBuilder;
    
    public GetItemsHandlerService(
        GetItems.GetItemsClient getItemsClient, 
        IMetadataBuilder metadataBuilder)
    {
        _getItemsClient = getItemsClient;
        _metadataBuilder = metadataBuilder;
    }
    
    public async Task<GetItemsReply> Handle(long userId)
    {
        var metadata = await _metadataBuilder.Build();
        
        return await _getItemsClient.GetAsync(new Empty(), metadata);
    }
}