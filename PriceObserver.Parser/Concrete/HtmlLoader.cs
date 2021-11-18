using System;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using PriceObserver.Parser.Abstract;
using HtmlLoadResult = PriceObserver.Parser.Models.HtmlLoadResult;

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
        
        public async Task<HtmlLoadResult> Load(Uri url)
        {
            var response = await _httpClient.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
                return HtmlLoadResult.Fail("Страница не найдена");
            
            var html = await response.Content.ReadAsStreamAsync();
            var htmlDocument =  await _htmlParser.ParseDocumentAsync(html);

            return HtmlLoadResult.Success(htmlDocument);
        }
    }
}