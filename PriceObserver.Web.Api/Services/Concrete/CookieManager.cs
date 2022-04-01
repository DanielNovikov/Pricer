﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PriceObserver.Web.Shared.Defaults;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.Api.Services.Concrete;

public class CookieManager : ICookieManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieManager(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<string?> GetValue(string key)
    {
        var value = _httpContextAccessor
            .HttpContext?
            .Request
            .Cookies[CookieKeys.AccessToken];

        return Task.FromResult(value);
    }

    public Task SetValue(string key, string value)
    {
        var cookies = _httpContextAccessor
            .HttpContext?
            .Response
            .Cookies;
        
        cookies?.Append(key, value);

        return Task.CompletedTask;
    }
}