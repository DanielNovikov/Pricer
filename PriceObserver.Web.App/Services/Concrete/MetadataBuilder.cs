using Grpc.Core;
using PriceObserver.Web.App.Services.Abstract;
using PriceObserver.Web.Shared.Defaults;
using PriceObserver.Web.Shared.Services.Abstract;

namespace PriceObserver.Web.App.Services.Concrete;

public class MetadataBuilder : IMetadataBuilder
{
    private readonly ICookieManager _cookieManager;

    private const string AuthorizationHeaderKey = "Authorization";
    private const string BearerAuthorizationTemplate = "Bearer {0}";
    
    public MetadataBuilder(ICookieManager cookieManager)
    {
        _cookieManager = cookieManager;
    }

    public async Task<Metadata?> Build()
    {
        var accessToken = await _cookieManager.GetValue(CookieKeys.AccessToken);

        if (string.IsNullOrEmpty(accessToken))
            return default;

        return new Metadata
        {
            { AuthorizationHeaderKey, string.Format(BearerAuthorizationTemplate, accessToken) }
        };
    }
}