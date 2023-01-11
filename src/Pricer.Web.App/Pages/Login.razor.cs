using Microsoft.AspNetCore.Components;
using Pricer.Web.Shared.Defaults;
using Pricer.Web.Shared.Grpc.HandlerServices;
using Pricer.Web.Shared.Services.Abstract;

namespace Pricer.Web.App.Pages;

public partial class Login : ComponentBase
{
    [Parameter] public Guid Token { get; set; }

    [Inject] public IAuthenticationHandlerService AuthenticationHandlerService { get; set; } = default!;

    [Inject] public NavigationManager NavigationManager { get; set; } = default!;

    [Inject] public ICookieManager CookieManager { get; set; } = default!;
    
    [Inject] public IPrerenderCache Cache { get; set; } = default!;

    private bool? _isAuthenticated = default;

    protected override async Task OnInitializedAsync()
    {
        var accessToken = await CookieManager.GetValue(CookieKeys.AccessToken);
        if (accessToken is not null)
        {
            NavigationManager.NavigateTo("/home", true);
            return;
        }

        var authenticationResult = await Cache.GetOrAdd(
            "authorization",
            async () =>
            {
                Console.WriteLine("Authorize user");
                
                return await AuthenticationHandlerService.Authenticate(Token);
            });
        
        if (!authenticationResult.IsSuccess)
        {
            _isAuthenticated = false;
            return;
        }

        accessToken = authenticationResult.AccessToken;
        await CookieManager.SetValue(CookieKeys.AccessToken, accessToken);
        NavigationManager.NavigateTo("/home", true);
    }
}