using System;
using System.Threading.Tasks;
using FluentAssertions;
using PriceObserver.Data.InMemory.Models.Enums;
using Xunit;

namespace PriceObserver.Parser.Tests.IntegrationTests;

public class ContentValidatorTests : IntegrationTestingBase
{
    [Theory]
    [InlineData("https://www.brocard.ua/ua/product/nabir-dsquared2-red-wood-134978", ShopKey.Brocard)]
    //[InlineData("https://www.farfetch.com/ua/shopping/men/alexander-mcqueen-iphone-xs-item-14620644.aspx?storeid=9359", ShopKey.Farfetch)]
    [InlineData("https://intertop.ua/ua/product/sneakers-clarks-4965745?tr_pr=analog", ShopKey.Intertop)]
    [InlineData("https://md-fashion.com.ua/store/zenskie-golubye-dzinsy-kiley-replay-wa434r000108-729-goluboj", ShopKey.MdFashion)]
    [InlineData("https://rozetka.com.ua/lg_75nano756pa/p292219333/", ShopKey.Rozetka)]
    [InlineData("https://stylus.ua/lg-55nano77-p803303c526.html", ShopKey.Stylus)]
    public async Task WhenIsNotAvailable_ShouldReturnOutOfStockError(string url, ShopKey shopKey)
    {   
        var uri = new Uri(url);

        var result = await Parser.Parse(uri, shopKey);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Parser_OutOfStock);
    }

    [Theory]
    [InlineData("https://intertop.ua/ua/", ShopKey.Adidas)]
    [InlineData("https://answear.ua/c/vin", ShopKey.Answear)]
    [InlineData("https://www.brocard.ua/", ShopKey.Brocard)]
    [InlineData("https://www.ctrs.com.ua/", ShopKey.Citrus)]
    //[InlineData("https://www.farfetch.com/ua/shopping/men/alexander-mcqueen/items.aspx", ShopKey.Farfetch)]
    [InlineData("https://intertop.ua/ua/", ShopKey.Intertop)]
    [InlineData("https://makeup.com.ua/", ShopKey.Makeup)]
    [InlineData("https://md-fashion.com.ua/", ShopKey.MdFashion)]
    [InlineData("https://modivo.ua/", ShopKey.Modivo)]
    [InlineData("https://rozetka.com.ua/", ShopKey.Rozetka)]
    [InlineData("https://stylus.ua/", ShopKey.Stylus)]
    public async Task WhenThereIsNoItemInfo_ShouldReturnNoItemInfoOnPageError(string url, ShopKey shopKey)
    {
        var uri = new Uri(url);

        var result = await Parser.Parse(uri, shopKey);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Parser_NoItemInfoOnPage);
    }
}