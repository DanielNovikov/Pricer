using Microsoft.JSInterop;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.App.Services.Concrete;

public class CookieManager : ICookieManager
{
    private readonly IJSRuntime _jsRuntime;

    private const string CookieSeparator = ";";
    private const char CookieKeyValueSeparator = '=';
    
    public CookieManager(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SetValue(string key, string value)
    {
        var cookie = $"{key}={value}; path=/";
        await _jsRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{cookie}\"");
    }

    public async Task Remove(string key)
    {
        var cookie = $"{key}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/";
        await _jsRuntime.InvokeVoidAsync("eval", $"document.cookie = \"{cookie}\"");
    }

    public async Task<string?> GetValue(string key)
    {
        var cookiesString = await _jsRuntime.InvokeAsync<string>("eval", "document.cookie");
        if (string.IsNullOrEmpty(cookiesString))
            return default;

        var cookies = cookiesString.Split(CookieSeparator);
        foreach (var cookie in cookies)
        {
            if (string.IsNullOrEmpty(cookie)) 
                continue;
            
            var cookieSeparatorIndex = cookie.IndexOf(CookieKeyValueSeparator);
            if (cookieSeparatorIndex < 0)
                continue;

            var cookieKey = cookie.Substring(0, cookieSeparatorIndex).Trim();
            if (cookieKey.Equals(key, StringComparison.OrdinalIgnoreCase))
                return cookie.Substring(cookieSeparatorIndex + 1);
        }

        return default;
    }
}