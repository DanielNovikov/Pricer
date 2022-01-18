﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PriceObserver.Api.Services.Abstract;
using PriceObserver.Api.Services.Models.Response;
using PriceObserver.Controllers.Base;

namespace PriceObserver.Controllers;

public class ItemController : AuthorizedControllerBase
{
    private readonly IApiItemService _itemService;

    public ItemController(IApiItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<IList<ShopVm>> Get()
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