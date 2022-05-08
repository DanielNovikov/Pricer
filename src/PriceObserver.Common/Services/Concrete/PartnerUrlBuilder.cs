using System;
using Microsoft.Extensions.Options;
using PriceObserver.Common.Models.Options;
using PriceObserver.Common.Services.Abstract;

namespace PriceObserver.Common.Services.Concrete;

public class PartnerUrlBuilder : IPartnerUrlBuilder
{
    private readonly WebsiteOptions _options;

    private const string PartnerUrlTemplate = "{0}/view?url={1}";
    
    public PartnerUrlBuilder(IOptionsSnapshot<WebsiteOptions> optionsSnapshot)
    {
        _options = optionsSnapshot.Value;
    }

    public string Build(Uri url)
    {
        return string.Format(PartnerUrlTemplate, _options.Url, url);
    }
}