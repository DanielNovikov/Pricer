using PriceObserver.Web.App.Services.Abstract;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;

namespace PriceObserver.Web.App.Services.Concrete;

public class DeleteItemHandlerService : IDeleteItemHandlerService
{
    private readonly DeleteItem.DeleteItemClient _client;
    private readonly IMetadataBuilder _metadataBuilder;

    public DeleteItemHandlerService(
        DeleteItem.DeleteItemClient client,
        IMetadataBuilder metadataBuilder)
    {
        _client = client;
        _metadataBuilder = metadataBuilder;
    }

    public async Task Handle(int itemId, long userId)
    {
        var metadata = await _metadataBuilder.Build();
        
        var request = new DeleteItemRequest
        {
            ItemId = itemId
        };

        await _client.DeleteAsync(request, metadata);
    }
}