using Microsoft.AspNetCore.Components;
using PriceObserver.Web.Shared.Defaults;
using PriceObserver.Web.Shared.Grpc.HandlerServices;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.App.Pages;

public partial class Login : ComponentBase
{
    [Parameter]
    public Guid Token { get; set; }

    [Inject]
    public IAuthenticationHandlerService AuthenticationHandlerService { get; set; }
    
    [Inject] 
    public NavigationManager NavigationManager { get; set; } = default!;
    
    [Inject]
    public ICookieManager CookieManager { get; set; } = default!;

    public bool Authorized { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        var authenticationResult = await AuthenticationHandlerService.Handle(Token);

        if (!authenticationResult.IsSuccess)
        {
            Authorized = false;
            return;
        }

        var accessToken = authenticationResult.AccessToken;
        await CookieManager.SetValue(CookieKeys.AccessToken, accessToken);
            
        NavigationManager.NavigateTo("/home");
    }
}