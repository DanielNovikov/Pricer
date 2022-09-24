using System;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Models;

namespace PriceObserver.Parser.Concrete;

public class HtmlLoader : IHtmlLoader
{
    private readonly IRequestHeadersBuilder _requestHeadersBuilder;
    private readonly HttpClient _httpClient;
    private readonly IHtmlParser _htmlParser;
        
    public HtmlLoader(
        IRequestHeadersBuilder requestHeadersBuilder,
        HttpClient httpClient,
        IHtmlParser htmlParser)
    {
        _requestHeadersBuilder = requestHeadersBuilder;
        _httpClient = httpClient;
        _htmlParser = htmlParser;
    }
        
    public async Task<HtmlLoadResult> Load(Uri url, ShopKey shopKey)
    {
        var requestHeaders = _requestHeadersBuilder.Build(url, shopKey);
        
        foreach (var (key, value) in requestHeaders)
            _httpClient.DefaultRequestHeaders.Add(key, value);
        
        var response = await _httpClient.GetAsync(url);
            
        if (!response.IsSuccessStatusCode)
            return HtmlLoadResult.Fail(ResourceKey.Parser_PageNotFound);
            
        var html = await response.Content.ReadAsStreamAsync();
        var htmlDocument = await _htmlParser.ParseDocumentAsync(html);

        return HtmlLoadResult.Success(htmlDocument);
    }
}