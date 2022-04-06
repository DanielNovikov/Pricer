using System.Net.Http.Headers;
using System.Net.Http.Json;
using PriceObserver.Web.App.Services.Abstract;
using PriceObserver.Web.Shared.Defaults;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.App.Services.Concrete;

public class AuthorizedHttpClient : IAuthorizedHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ICookieManager _cookieManager;

    public AuthorizedHttpClient(
        HttpClient httpClient,
        ICookieManager cookieManager)
    {
        _httpClient = httpClient;
        _cookieManager = cookieManager;
    }

    public async Task<TType> Get<TType>(string endpoint)
    {
        await AuthorizeHttpClient(_httpClient);

        return await _httpClient.GetFromJsonAsync<TType>(endpoint) ??
            throw new InvalidOperationException($"Unable to get data by endpoint '{endpoint}'");
    }

    public async Task Delete(string endpoint)
    {
        await AuthorizeHttpClient(_httpClient);

        await _httpClient.DeleteAsync(endpoint);
    }

    private async Task AuthorizeHttpClient(HttpClient httpClient)
    {
        var token = await _cookieManager.GetValue(CookieKeys.AccessToken);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}