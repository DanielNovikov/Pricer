using System;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using Microsoft.Extensions.Logging;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;
using Pricer.Parser.Models;

namespace Pricer.Parser.Concrete;

public class HtmlLoader : IHtmlLoader
{
    private readonly IRequestHeadersBuilder _requestHeadersBuilder;
    private readonly IHtmlParser _htmlParser;
    private readonly IDefaultHttpClient _defaultHttpClient;
    private readonly IProxyHttpClient _proxyHttpClient;
    private readonly ILogger<HtmlLoader> _logger;
        
    public HtmlLoader(
        IRequestHeadersBuilder requestHeadersBuilder,
        IHtmlParser htmlParser, 
        IDefaultHttpClient defaultHttpClient,
        IProxyHttpClient proxyHttpClient,
        ILogger<HtmlLoader> logger)
    {
        _requestHeadersBuilder = requestHeadersBuilder;
        _htmlParser = htmlParser;
        _defaultHttpClient = defaultHttpClient;
        _proxyHttpClient = proxyHttpClient;
        _logger = logger;
    }
        
    public async Task<HtmlLoadResult> Load(Uri url, ShopKey shopKey)
    {
        var requestHeaders = _requestHeadersBuilder.Build(url, shopKey);

        var response = await _defaultHttpClient.Get(url, requestHeaders);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Couldn't load page by default client, trying with proxy...\n{0}", url);
            response = await _proxyHttpClient.Get(url, requestHeaders);

            if (!response.IsSuccessStatusCode)
                return HtmlLoadResult.Fail(ResourceKey.Parser_PageNotFound);
        }
            
        
        #if DEBUG

        var html = await response.Content.ReadAsByteArrayAsync();
        var htmlString = Encoding.UTF8.GetString(html);
        var htmlDocument = await _htmlParser.ParseDocumentAsync(htmlString);
        
        #else
        
        var html = await response.Content.ReadAsStreamAsync();
        var htmlDocument = await _htmlParser.ParseDocumentAsync(html);
        
        #endif

        return HtmlLoadResult.Success(htmlDocument);
    }
}