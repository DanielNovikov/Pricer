using Microsoft.AspNetCore.Components;
using Pricer.Models;

namespace Pricer.Components.Client;

public partial class ClientShop
{
    [Parameter] public ShopDto Shop { get; set; } = default!;
    [Parameter] public EventCallback<ShopDto> OnDeleted { get; set; }

    private void OnItemDeleted(ItemDto item)
    {
        Shop.Items.Remove(item);

        if (!Shop.Items.Any())
            OnDeleted.InvokeAsync(Shop);
    }
}