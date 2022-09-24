using System;
using System.Threading.Tasks;
using FluentAssertions;
using Pricer.Data.InMemory.Models.Enums;
using Xunit;

namespace Pricer.Parser.FunctionalTests;

public class ContentValidatorTests : TestBase
{
    [Theory]
    [InlineData("https://www.adidas.ua/", ShopKey.Adidas)]
    [InlineData("https://allo.ua/", ShopKey.Allo)]
    [InlineData("https://answear.ua/c/vin", ShopKey.Answear)]
    [InlineData("https://www.ctrs.com.ua/", ShopKey.Citrus)]
    [InlineData("https://comfy.ua/", ShopKey.Comfy)]
    [InlineData("https://epicentrk.ua/", ShopKey.Epicentr)]
    [InlineData("https://estore.ua/", ShopKey.Estore)]
    [InlineData("https://eva.ua/", ShopKey.Eva)]
    //[InlineData("https://www.farfetch.com/ua/shopping/men/alexander-mcqueen/items.aspx", ShopKey.Farfetch)]
    [InlineData("https://intertop.ua/ua/", ShopKey.Intertop)]
    [InlineData("https://jysk.ua/", ShopKey.Jysk)]
    [InlineData("https://maudau.com.ua/", ShopKey.MauDau)]
    [InlineData("https://makeup.com.ua/", ShopKey.Makeup)]
    [InlineData("https://md-fashion.com.ua/", ShopKey.MdFashion)]
    [InlineData("https://modivo.ua/", ShopKey.Modivo)]
    [InlineData("https://www.moyo.ua/", ShopKey.Moyo)]
    [InlineData("https://www.notino.ua/", ShopKey.Notino)]
    [InlineData("https://www.olx.ua/d/uk/obyavlenie/pivnaya-kruzhka-zatecky-gus-1l-obmen-na-hoegaarden-0-5l-IDgRJMv.html", ShopKey.Olx)]
    [InlineData("https://www.olx.ua", ShopKey.Olx)]
    [InlineData("https://e-pandora.ua/", ShopKey.Pandora)]
    [InlineData("https://prom.ua/", ShopKey.Prom)]
    [InlineData("https://prostor.ua/", ShopKey.Prostor)]
    [InlineData("https://rozetka.com.ua/", ShopKey.Rozetka)]
    [InlineData("https://shafa.ua/", ShopKey.Shafa)]
    [InlineData("https://athletics.kiev.ua/", ShopKey.Athletics)]
    [InlineData("https://stylus.ua/", ShopKey.Stylus)]
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