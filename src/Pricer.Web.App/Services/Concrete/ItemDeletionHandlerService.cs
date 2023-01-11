using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;
using Pricer.Web.App.Services.Abstract;

namespace Pricer.Web.App.Services.Concrete;

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

    public async Task Delete(int itemId, int userId)
    {
        var metadata = await _metadataBuilder.Build();
        
        var request = new ItemDeletionRequest
        {
            ItemId = itemId
        };

        await _client.DeleteAsync(request, metadata);
    }
}