using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Pricer.Data.InMemory.Models;
using Pricer.Data.InMemory.Repositories.Abstract;
using Pricer.Data.Service.Abstract;

namespace Pricer.Pages;

public partial class Index
{
    private IList<ShopCategory>? _shopCategories;
    private ShopCategory? _currentShopCategory;

    [Inject] public IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] public IShopCategoryRepository ShopCategoryRepository { get; set; } = default!;
    [Inject] public IResourceService ResourceService { get; set; } = default!; 
    
    protected override void OnInitialized()
    {
        _shopCategories = ShopCategoryRepository.GetAll();
        _currentShopCategory = _shopCategories.First();
    }

    private void ChangeShopCategory(ShopCategory shopCategory)
    {
        _currentShopCategory = shopCategory;
    }

    private async Task ScrollToBanner()
    {
        await ScrollTo("banner-container");
    }
    
    private async Task ScrollToFunctions()
    {
        await ScrollTo("functions-container");
    }
    
    private async Task ScrollToVideo()
    {
        await ScrollTo("video-header");
    }
    
    private async Task ScrollToShops()
    {
        await ScrollTo("shops-header");
    }

    private async Task ScrollTo(string id)
    {
        await JsRuntime.InvokeVoidAsync("scrollToView", id);
    }
}