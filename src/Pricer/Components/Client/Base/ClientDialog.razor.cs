using Microsoft.AspNetCore.Components;

namespace Pricer.Components.Client.Base;

public partial class ClientDialog
{
    private bool _isDialogVisible;

    [Parameter] public RenderFragment Content { get; set; } = default!;

    [Parameter] public EventCallback? OnDialogClosed { get; set; }

    public void OpenDialog()
    {
        _isDialogVisible = true;
    }
    
    public void CloseDialog()
    {
        _isDialogVisible = false;
        OnDialogClosed?.InvokeAsync();
    }
}