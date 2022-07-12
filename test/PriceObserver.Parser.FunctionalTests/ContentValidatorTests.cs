using System;
using System.Threading.Tasks;
using FluentAssertions;
using PriceObserver.Data.InMemory.Models.Enums;
using Xunit;

namespace PriceObserver.Parser.FunctionalTests;

public class ContentValidatorTests : TestBase
{
    [Theory]
    [InlineData("https://auchan.zakaz.ua/uk/products/04823061323897/iogurt-chudo-270g-ukrayina/", ShopKey.Auchan)]
    [InlineData("https://comfy.ua/televizor-lg-43un81006lb.html", ShopKey.Comfy)]
    [InlineData("https://eko.zakaz.ua/uk/products/04820045704536/sir-molokiia-350g-ukrayina/", ShopKey.EkoMarket)]
    [InlineData("https://estore.ua/apple-watch-series-3-nike-42mm-gps-space-gray-aluminium-case-with-anthracite-black-nike-sport-band-mtf42", ShopKey.Estore)]
    //[InlineData("https://www.farfetch.com/ua/shopping/men/alexander-mcqueen-iphone-xs-item-14620644.aspx?storeid=9359", ShopKey.Farfetch)]
    [InlineData("https://intertop.ua/ua/product/sneakers-clarks-4965745?tr_pr=analog", ShopKey.Intertop)]
    [InlineData("https://md-fashion.com.ua/store/zenskie-golubye-dzinsy-kiley-replay-wa434r000108-729-goluboj", ShopKey.MdFashion)]
    [InlineData("https://megamarket.zakaz.ua/uk/products/04820178810401/vershki-organik-milk-180g/", ShopKey.MegaMarket)]
    [InlineData("https://www.moyo.ua/televizor-lg-75sm9000pla/448309.html", ShopKey.Moyo)]
    [InlineData("https://novus.zakaz.ua/uk/products/04820184660571/iogurt-350g/", ShopKey.Novus)]
    [InlineData("https://rozetka.com.ua/lg_75nano756pa/p292219333/", ShopKey.Rozetka)]
    [InlineData("https://stylus.ua/lg-55nano77-p803303c526.html", ShopKey.Stylus)]
    [InlineData("https://stolychnyi.zakaz.ua/uk/products/stolychnyi02010000295007/iogurt-500ml/", ShopKey.StolychnyiRynok)]
    [InlineData("https://telemart.ua/products/lg-315-ultrafine-32un650-w-blacksilver/", ShopKey.Telemart)]
    [InlineData("https://ultramarket.zakaz.ua/uk/products/04823061322982/iogurt-romol-270g/", ShopKey.UltraMarket)]
    [InlineData("https://varus.zakaz.ua/uk/products/04823065726878/iogurt-fanni-280g/", ShopKey.Varus)]
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
    [InlineData("https://www.ctrs.com.ua/", ShopKey.Citrus)]
    [InlineData("https://comfy.ua/", ShopKey.Comfy)]
    [InlineData("https://estore.ua/", ShopKey.Estore)]
    //[InlineData("https://www.farfetch.com/ua/shopping/men/alexander-mcqueen/items.aspx", ShopKey.Farfetch)]
    [InlineData("https://intertop.ua/ua/", ShopKey.Intertop)]
    [InlineData("https://jysk.ua/", ShopKey.Jysk)]
    [InlineData("https://makeup.com.ua/", ShopKey.Makeup)]
    [InlineData("https://md-fashion.com.ua/", ShopKey.MdFashion)]
    [InlineData("https://modivo.ua/", ShopKey.Modivo)]
    [InlineData("https://www.moyo.ua/", ShopKey.Moyo)]
    [InlineData("https://rozetka.com.ua/", ShopKey.Rozetka)]
    [InlineData("https://athletics.kiev.ua/", ShopKey.Athletics)]
    //[InlineData("https://stylus.ua/", ShopKey.Stylus)]
    [InlineData("https://telemart.ua/", ShopKey.Telemart)]
    [InlineData("https://www.watsons.ua/", ShopKey.Watsons)]
    public async Task WhenThereIsNoItemInfo_ShouldReturnNoItemInfoOnPageError(string url, ShopKey shopKey)
    {
        var uri = new Uri(url);

        var result = await Parser.Parse(uri, shopKey);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Parser_NoItemInfoOnPage);
    }
}