using Microsoft.AspNetCore.Components;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Models;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.App.Components;

public partial class Items : ComponentBase
{
    [EditorRequired]
    [Parameter]
    public IList<ShopItemsResponseModel> Data { get; set; }
    
    [Parameter]
    public EventCallback<int> OnItemDeleted { get; set; }

    private string ItemImageModalSource { get; set; }
    private bool ItemImageModalVisible { get; set; }
    
    public void ShowItemImageModal(string itemImageModalSource)
    {
        ItemImageModalSource = itemImageModalSource;
        ItemImageModalVisible = true;
    }

    public void CloseItemImageModal()
    {
        ItemImageModalVisible = false;
    }
}