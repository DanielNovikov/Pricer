using Microsoft.AspNetCore.Components;

namespace PriceObserver.App.Pages;

public class LoginPageModel
{
    [Parameter]
    public Guid Token { get; set; }
}