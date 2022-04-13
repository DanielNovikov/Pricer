using PriceObserver.Web.App.Services.Abstract;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;

namespace PriceObserver.Web.App.Services.Concrete;

public class ItemDeletionHandlerService : IItemDeletionHandlerService
{
    private readonly ItemDeletion.ItemDeletionClient _client;
    private readonly IMetadataBuilder _metadataBuilder;

    public ItemDeletionHandlerService(
        ItemDeletion.ItemDeletionClient client,
        IMetadataBuilder metadataBuilder)
    {
        _client = client;
        _metadataBuilder = metadataBuilder;
    }

    public async Task Delete(int itemId, long userId)
    {
        var metadata = await _metadataBuilder.Build();
        
        var request = new ItemDeletionRequest()
        {
            ItemId = itemId
        };

        await _client.DeleteAsync(request, metadata);
    }
}