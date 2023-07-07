using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Pricer.Components.Client.Base;
using Pricer.Components.Client.Dialogs;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Data.Service.Abstract;
using Pricer.Models;
using Pricer.Services.Abstract;

namespace Pricer.Components.Client;

public partial class ClientItem
{
    private ClientDeleteItemDialog _deleteItemDialog = default!;

    private List<ItemPriceChangeDto>? _priceChanges;
    private ClientItemPriceChangesDialog _itemPriceChangesDialog = default!;
    
    [Parameter] public ItemDto Item { get; set; } = default!;

    [Parameter] public EventCallback<ItemDto> OnDeleted { get; set; }

    [Inject] public IItemService ItemService { get; set; } = default!;
    [Inject] public IUserItemsService UserItemsService { get; set; } = default!;
    [Inject] public IJSRuntime JsRuntime { get; set; } = default!;

    private async Task OnGraphicsClicked()
    {
        _priceChanges = await UserItemsService.GetPriceChanges(Item.Id);
        _itemPriceChangesDialog.OpenDialog();
        
        StateHasChanged();
    }
    
    private void OnOpenDeleteDialogClicked()
    {
        _deleteItemDialog.OpenDialog();
    }
    
    private async Task OnDeleteClicked()
    {
        await ItemService.Delete(Item.Id);
        await OnDeleted.InvokeAsync(Item);
    }

    private async Task NavigateTo(Uri url)
    {
        await JsRuntime.InvokeVoidAsync("open", url.ToString(), "_blank");
    }
}