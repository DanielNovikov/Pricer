using System;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using PriceObserver.Parser.Abstract;

namespace PriceObserver.Parser.Concrete
{
    public class HtmlLoader : IHtmlLoader
    {
        private readonly HttpClient _httpClient;
        private readonly IHtmlParser _htmlParser;
        
        public HtmlLoader(HttpClient httpClient, IHtmlParser htmlParser)
        {
            _httpClient = httpClient;
            _htmlParser = htmlParser;
        }
        
        public async Task<IHtmlDocument> Load(Uri url)
        {
            var response = await _httpClient.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
                throw new Exception("Page was not found");
            
            var html = await response.Content.ReadAsStreamAsync();
            return await _htmlParser.ParseDocumentAsync(html);
        }
    }
}