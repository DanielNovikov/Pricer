using Microsoft.AspNetCore.Mvc;
using PriceObserver.Web.Api.Controllers.Base;
using PriceObserver.Web.Api.Models;
using PriceObserver.Web.Api.Services.Abstract;

namespace PriceObserver.Web.Api.Controllers;

[Route("api/item")]
public class ItemController : AuthorizedControllerBase
{
    private readonly IApiItemService _itemService;

    public ItemController(IApiItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<IList<ItemsVm>> Get()
    {
        var userId = GetUserId();
        return await _itemService.GetByUserId(userId);
    }

    [HttpDelete("{id:int}")]
    public async Task Delete(int id)
    {
        var userId = GetUserId();
        await _itemService.Delete(id, userId);
    }
}