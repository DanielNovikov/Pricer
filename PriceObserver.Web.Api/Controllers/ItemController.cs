using Microsoft.AspNetCore.Mvc;
using PriceObserver.Web.Api.Controllers.Base;
using PriceObserver.Web.Shared.Models;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.Api.Controllers;

[Route("api")]
public class ItemController : AuthorizedControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet("items")]
    public async Task<IList<ItemsVm>> Get()
    {
        var userId = GetUserId();
        return await _itemService.GetByUserId(userId);
    }

    [HttpDelete("item/{id:int}")]
    public async Task Delete(int id)
    {
        var userId = GetUserId();
        await _itemService.Delete(id, userId);
    }
}