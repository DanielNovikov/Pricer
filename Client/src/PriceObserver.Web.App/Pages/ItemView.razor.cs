using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;

namespace PriceObserver.Web.App.Pages;

public partial class ItemView : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    [Inject] public IJSRuntime JsRuntime { get; set; } = default!;

    private const string ItemLinkElementIdentifier = "item-link";
    private const string ItemUrlQueryParameterKey = "url";
    private string _itemUrl = default!;

    protected override void OnInitialized()
    {
        var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);
        var query = QueryHelpers.ParseQuery(uri.Query);

        if (!query.TryGetValue(ItemUrlQueryParameterKey, out var itemUrlParameter))
        {
            NavigationManager.NavigateTo("/");
            return;
        }

        _itemUrl = itemUrlParameter.Single();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await JsRuntime.InvokeVoidAsync("redirectToPartnerUrl", ItemLinkElementIdentifier);
    }
}