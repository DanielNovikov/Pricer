using System;
using Microsoft.Extensions.Options;
using Pricer.Common.Models.Options;
using Pricer.Common.Services.Abstract;

namespace Pricer.Common.Services.Concrete;

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
#if DEBUG
		return url.ToString();
#else
        //return string.Format(PartnerUrlTemplate, _options.Url, url);
		return url.ToString();
#endif
	}
}