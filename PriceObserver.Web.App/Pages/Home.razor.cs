﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PriceObserver.Web.Shared.Defaults;
using PriceObserver.Web.Shared.Grpc;
using PriceObserver.Web.Shared.Grpc.HandlerServices;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.App.Pages;

public partial class Home : ComponentBase
{
    [Inject]
    public ICookieManager CookieManager { get; set; }
    
    [Inject]
    public IAuthenticationService AuthenticationService { get; set; }

    [Inject]
    public IGetItemsHandlerService GetItemsHandlerService { get; set; }

    [Inject]
    public IDeleteItemHandlerService DeleteItemHandlerService { get; set; }
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    [Inject]
    public IJSRuntime JsRuntime { get; set; }

    private IList<ShopItemsResponseModel> ItemsData { get; set; } = new List<ShopItemsResponseModel>();
    
    protected override async Task OnInitializedAsync()
    {
        var accessToken = await CookieManager.GetValue(CookieKeys.AccessToken);

        if (string.IsNullOrEmpty(accessToken))
        {
            NavigationManager.NavigateTo("/");
            return;
        }
        
        var userId = AuthenticationService.GetUserId(accessToken);
        await LoadItemsData(userId);
    }

    private async Task DeleteItem(int id)
    {
        var accessToken = await CookieManager.GetValue(CookieKeys.AccessToken);

        if (string.IsNullOrEmpty(accessToken))
            throw new InvalidOperationException("Access token has been cleared");

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
        
        var userId = AuthenticationService.GetUserId(accessToken);
        await DeleteItemHandlerService.Handle(id, userId);
        await LoadItemsData(userId);
    }

    private async Task Logout()
    {
        await CookieManager.Remove(CookieKeys.AccessToken);
        NavigationManager.NavigateTo("https://t.me/pricer_official_bot");
    }

    private async Task LoadItemsData(long userId)
    {
        var response = await GetItemsHandlerService.Handle(userId);
        ItemsData = response.Data;
    }
}