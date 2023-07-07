using Microsoft.AspNetCore.Components;
using Pricer.Data.Persistent.Models;
using Pricer.Data.Persistent.Repositories.Abstract;
using Pricer.Services.Abstract;

namespace Pricer.Shared.Client.Base;

public abstract class ClientComponentBase : ComponentBase
{
    [Inject] protected IAuthenticationService AuthenticationService { get; set; } = default!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = default!;
    [Inject] protected IUserRepository UserRepository { get; set; } = default!;

    protected bool IsInitialized;
    protected int UserId;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (IsInitialized)
            return;

        var userId = await AuthenticationService.GetUserId();
        if (!userId.HasValue)
        {
            NavigationManager.NavigateTo("/", true);
            return;
        }

        UserId = userId.Value;
        IsInitialized = true;

        await OnPageInitialized();
    }

    protected async Task<User> GetUser()
    {
        return await UserRepository.GetById(UserId);
    }

    protected virtual Task OnPageInitialized()
    {
        return Task.CompletedTask;
    }
}