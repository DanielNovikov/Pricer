using System;
using System.Threading.Tasks;
using FluentAssertions;
using PriceObserver.Data.InMemory.Models.Enums;
using Xunit;

namespace PriceObserver.Parser.Tests.IntegrationTests;

public class ServiceTests : IntegrationTestingBase
{
    [Fact]
    public async Task WhenPageCouldNotBeFound_ShouldReturnPageNotFoundError()
    {
        var url = new Uri("https://intertop.ua/ua/product/not-existing-product");
        var shopKey = ShopKey.Intertop;

        var result = await Parser.Parse(url, shopKey);

        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(ResourceKey.Parser_PageNotFound);
    }
}