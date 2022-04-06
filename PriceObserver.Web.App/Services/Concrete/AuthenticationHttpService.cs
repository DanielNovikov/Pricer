using System.Net.Http.Json;
using PriceObserver.Web.Shared.Models;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.App.Services.Concrete;

public class AuthenticationHttpService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    private const string AuthenticationEndpoint = "/api/authentication/{0}";
    
    public AuthenticationHttpService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AuthenticationServiceResult> Authenticate(Guid token)
    {
        var endpoint = string.Format(AuthenticationEndpoint, token);

        var response = await _httpClient.PostAsync(endpoint, null);

        if (!response.IsSuccessStatusCode)
            return AuthenticationServiceResult.Fail();

        var responseModel = await response.Content.ReadFromJsonAsync<AuthenticationResponseModel>() ??
            throw new ArgumentNullException(nameof(response.Content));
        
        return AuthenticationServiceResult.Success(responseModel);
    }

    public long GetUserId(string accessToken)
    {
        return default;
    }
}