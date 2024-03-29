﻿using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Pricer.Common.Models.Options;
using Pricer.Data.Service.Abstract;
using Pricer.Dialog.Services.Abstract;

namespace Pricer.Dialog.Services.Concrete;

public class WebsiteLoginUrlBuilder : IWebsiteLoginUrlBuilder
{
    private readonly IUserTokenService _userTokenService;
    private readonly WebsiteOptions _websiteOptions;

    private const string LoginEndpoint = "{0}/login/{1}";

    public WebsiteLoginUrlBuilder(
        IUserTokenService userTokenService, 
        IOptionsSnapshot<WebsiteOptions> websiteCommandOptionsSnapshot)
    {
        _userTokenService = userTokenService;
        _websiteOptions = websiteCommandOptionsSnapshot.Value;
    }

    public async Task<string> Build(int userId)
    {
        var userToken = await _userTokenService.CreateForUser(userId);

        return string.Format(LoginEndpoint, _websiteOptions.Url, userToken.Token);
    }
}