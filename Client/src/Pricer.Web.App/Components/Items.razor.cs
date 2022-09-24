using Microsoft.AspNetCore.Components;
using PriceObserver.Web.Shared.Grpc;

namespace Pricer.Web.App.Components;

public partial class Items : ComponentBase
{
    [EditorRequired]
    [Parameter]
    public IList<ShopItemsResponseModel> Data { get; set; } = default!;
    
    [Parameter]
    public EventCallback<int> OnItemDeleted { get; set; }

    private string ItemImageModalSource { get; set; } = default!;
    private bool ItemImageModalVisible { get; set; } = default!;

    private void ShowItemImageModal(string itemImageModalSource)
    {
        ItemImageModalSource = itemImageModalSource;
        ItemImageModalVisible = true;
    }

    private void CloseItemImageModal()
    {
        ItemImageModalVisible = false;
    }
}