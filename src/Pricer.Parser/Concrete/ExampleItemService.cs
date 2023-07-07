using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Pricer.Data.InMemory.Models.Enums;
using Pricer.Parser.Abstract;
using Pricer.Parser.Models;

namespace Pricer.Parser.Concrete;

public class ExampleItemService : IExampleItemService
{
    private const string ExampleSource = "https://intertop.ua/ua/catalog/zhenskaya_obuv/brand-lacoste_tommy-hilfiger-seller_type-mti/";

    private readonly IHtmlLoader _htmlLoader;
    private readonly ILogger<ExampleItemService> _logger;
    private readonly IMemoryCache _memoryCache;

    public ExampleItemService(
        IHtmlLoader htmlLoader,
        ILogger<ExampleItemService> logger,
        IMemoryCache memoryCache)
    {
        _htmlLoader = htmlLoader;
        _logger = logger;
        _memoryCache = memoryCache;
    }
    
    public ShopKey ShopKey => ShopKey.Intertop;

    public async Task<ExampleResult> Get()
    {
        return await _memoryCache.GetOrCreateAsync("ExampleUrl", async (cacheEntry) =>
        {
            cacheEntry.SlidingExpiration = TimeSpan.FromDays(1);
            
            var document = await _htmlLoader.Load(new Uri(ExampleSource), ShopKey);

            if (!document.IsSuccess)
            {
                _logger.LogWarning("EXAMPLE. Can't get example page.");
                return ExampleResult.Fail();
            }

            var itemElements = document.Result
                .QuerySelectorAll<IHtmlElement>("div.product-list > div")
                .Where(x => x.QuerySelector("div.product-price") != null)
                .ToList();

            if (!itemElements.Any())
            {
                _logger.LogWarning("EXAMPLE. There are no items on page.");
                return ExampleResult.Fail();
            }

            var itemElement = itemElements.FirstOrDefault(x => x.QuerySelector("div.was-price") == null);
            if (itemElement == null)
            {
                _logger.LogWarning("EXAMPLE. Can't get item element without discount.");
                itemElement = itemElements.FirstOrDefault();

                if (itemElement == null)
                {
                    _logger.LogWarning("EXAMPLE. Can't get item element either without and with discount.");
                    return ExampleResult.Fail();
                }
            }

            var linkElement = itemElement.QuerySelector<IHtmlElement>("div.product-thumb > a");
            if (linkElement == null)
            {
                _logger.LogWarning("EXAMPLE. Can't get link element of item element.");
                return ExampleResult.Fail();
            }

            var link = linkElement.GetAttribute("href");
            if (string.IsNullOrEmpty(link))
            {
                _logger.LogWarning("EXAMPLE. Can't get href attribute of link element.");
                return ExampleResult.Fail();
            }

            var url = new Uri(link);
            return ExampleResult.Success(url);
        });
    }
}