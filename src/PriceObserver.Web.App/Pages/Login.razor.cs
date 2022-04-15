using Microsoft.AspNetCore.Components;
using PriceObserver.Web.Shared.Defaults;
using PriceObserver.Web.Shared.Grpc.HandlerServices;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.App.Pages;

public partial class Login : ComponentBase
{
    [Parameter] public Guid Token { get; set; }

    [Inject] public IAuthenticationHandlerService AuthenticationHandlerService { get; set; } = default!;

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    [Inject] public ICookieManager CookieManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var accessToken = await CookieManager.GetValue(CookieKeys.AccessToken);
        if (accessToken is not null)
        {
            NavigateToHome();
            return;
        }

        var authenticationResult = await AuthenticationHandlerService.Authenticate(Token);

        if (!authenticationResult.IsSuccess)
            return;

        accessToken = authenticationResult.AccessToken;
        await CookieManager.SetValue(CookieKeys.AccessToken, accessToken);
        NavigateToHome();
    }

    private void NavigateToHome()
    {
        NavigationManager.NavigateTo("/home");
    }
}