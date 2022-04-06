using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PriceObserver.Web.Shared.Defaults;
using PriceObserver.Web.Shared.Models;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.App.Pages;

public partial class Home : ComponentBase
{
    [Inject]
    public ICookieManager CookieManager { get; set; }
    
    [Inject]
    public IAuthenticationService AuthenticationService { get; set; }
    
    [Inject]
    public IItemService ItemService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public IJSRuntime JsRuntime { get; set; }

    private IList<ItemsVm> ItemsData { get; set; } = new List<ItemsVm>();
    
    protected override async Task OnInitializedAsync()
    {
        var accessToken = await CookieManager.GetValue(CookieKeys.AccessToken);

        if (string.IsNullOrEmpty(accessToken))
        {
            NavigationManager.NavigateTo("/");
            return;
        }
        
        var userId = AuthenticationService.GetUserId(accessToken);
        ItemsData = await ItemService.GetByUserId(userId);
    }

    private async Task DeleteItem(int id)
    {
        var accessToken = await CookieManager.GetValue(CookieKeys.AccessToken);

        if (string.IsNullOrEmpty(accessToken))
            throw new InvalidOperationException("Access token has been cleared");
        
        var userId = AuthenticationService.GetUserId(accessToken);
        await ItemService.Delete(id, userId);

        var itemsVm = ItemsData.Single(x => x.Items.Any(y => y.Id == id));

        if (itemsVm.Items.Count == 1)
        {
            ItemsData = ItemsData
                .Where(x => x != itemsVm)
                .ToList();
            
            return;
        }

        var itemToDelete = itemsVm.Items.Single(x => x.Id == id);
        itemsVm.Items.Remove(itemToDelete);
    }

    private async Task Logout()
    {
        await CookieManager.Remove(CookieKeys.AccessToken);
        NavigationManager.NavigateTo("https://t.me/pricer_official_bot");
    }
}