using System;
using System.Threading.Tasks;
using FluentAssertions;
using PriceObserver.Data.InMemory.Models.Enums;
using PriceObserver.Parser.Abstract;
using PriceObserver.Parser.Tests.IntegrationTests.Base;
using Xunit;

namespace PriceObserver.Parser.Tests.IntegrationTests.Intertop;

public class Parse_WhenItemIsAvailableAndHasInfo_ShouldParsePage : IntegrationTestingBase
{
    private readonly IParser _sut;

    public Parse_WhenItemIsAvailableAndHasInfo_ShouldParsePage()
    {
        _sut = GetService<IParser>();
    }

    [Fact]
    public async Task Test()
    {
        var url = new Uri("https://intertop.ua/ua/product/sweaters-and-sweaters-adidas-5701343");
        var shopKey = ShopKey.Intertop;

        var result = await _sut.Parse(url, shopKey);

        result.IsSuccess.Should().BeTrue();
        result.Result.ShopKey.Should().Be(shopKey);
        result.Result.ImageUrl.Should().Be(new Uri("https://intertop.ua/load/CN1097/MAIN.jpg"));
        result.Result.Price.Should().NotBe(0);
        result.Result.Title.Should().Be("Кофта спортивна Adidas M ZNE WV COLDFZ");
    }
}