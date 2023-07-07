using Pricer.Shared.Client.Base;

namespace Pricer.Components.Client.Base;

public abstract class ClientDialogBase : ClientComponentBase
{
    protected ClientDialog DialogRef = default!; 

    public virtual void OpenDialog() => DialogRef.OpenDialog();

    public virtual void CloseDialog() => DialogRef.CloseDialog();
}