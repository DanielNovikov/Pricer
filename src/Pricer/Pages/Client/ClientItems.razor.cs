using Microsoft.AspNetCore.Components;
using Pricer.Models;
using Pricer.Services.Abstract;

namespace Pricer.Pages.Client;

public partial class ClientItems
{
    private List<ShopDto>? _shops;

    [Inject] IUserItemsService UserItemsService { get; set; } = default!;
    
    protected override async Task OnPageInitialized()
    {
        _shops = await UserItemsService.GetByUserId(UserId);
        StateHasChanged();
    }

    private void OnShopDeleted(ShopDto shop)
    {
        _shops?.Remove(shop);
    }
}