using Microsoft.AspNetCore.Components;
using PriceObserver.Web.Shared.Defaults;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.App.Pages;

public partial class Home : ComponentBase
{   
    [Inject]
    public ICookieManager CookieManager { get; set; } = default!;
    
    [Inject]
    public IUserAuthenticationService UserAuthenticationService { get; set; } = default!;

    [Inject]
    public IItemsReceptionHandlerService ItemsReceptionHandlerService { get; set; } = default!;

    [Inject]
    public IItemDeletionHandlerService ItemDeletionHandlerService { get; set; } = default!;
    
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public IPrerenderCache Cache { get; set; } = default!;
    
    private IList<ShopItemsResponseModel> ItemsData { get; set; } = new List<ShopItemsResponseModel>();
    
    protected override async Task OnInitializedAsync()
    {
        var accessToken = await CookieManager.GetValue(CookieKeys.AccessToken);

        if (string.IsNullOrEmpty(accessToken))
        {
            NavigationManager.NavigateTo("/");
            return;
        }

        ItemsData = await Cache.GetOrAdd("items", async () =>
        {
            var userId = UserAuthenticationService.GetUserId(accessToken);
            var response = await ItemsReceptionHandlerService.Receive(userId);
            return response.Data;
        });
    }

    private async Task DeleteItem(int id)
    {
        var accessToken = await CookieManager.GetValue(CookieKeys.AccessToken);

        if (string.IsNullOrEmpty(accessToken))
            throw new InvalidOperationException("Access token has been cleared");

        RemoveItemFromView(id);

        var userId = UserAuthenticationService.GetUserId(accessToken);
        await ItemDeletionHandlerService.Delete(id, userId);
    }

    private async Task Logout()
    {
        await CookieManager.Remove(CookieKeys.AccessToken);
        NavigationManager.NavigateTo("https://t.me/pricer_official_bot");
    }

    private void RemoveItemFromView(int id)
    {
        var shopItems = ItemsData.First(x => x.Items.Any(y => y.Id == id));
        
        if (shopItems.Items.Count == 1)
        {
            ItemsData.Remove(shopItems);
        }
        else
        {
            var itemToDelete = shopItems.Items.First(x => x.Id == id);
            shopItems.Items.Remove(itemToDelete);
        }

        StateHasChanged();
    }
}